using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject moodVisual;
    private GameObject targetManagerObj;
    //public bool isInteractive =false;
    public bool isToRemove = false;
    //public bool hasInteracted = false;
    public string task;
    public string prevTask;
    public int mood;    //scala da 1 a 4 dove 1 è un mood positivo e 4 è negativo
    public Transform[] spawnPos;
    private SpriteRenderer moodSprite;

    public TargetManager targetManager;
    private StateMachine _stateMachine;

    public Transform Target;
    //public EnvInteractable Target { get; set; }

    public List<string> NRFinishedTasks = new List<string>();     //lista di task non ripetibli effettuati

    public GameObject targetObject;
    public bool fineInteract = false;

    public bool isQueuing = false;

    public Action<Avatar> InteractionCompleted;     //evento che indica quando un avatar ha finito l'interazione
                                                    //usato per il dequque degli avatar in fila
    public bool maxQueue=false;
    public Transform exclude;
    public int maxQueueCount = 0;
    private void Start()
    {
        //targetManagerObj = GameObject.Find("Target Points");
        //targetManager = targetManagerObj.GetComponent<TargetManager>();

        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        var scheduler = GameObject.Find("Interactables").GetComponent<InteractablesManager>();

        _stateMachine = new StateMachine();

        var findInteractable= new FindNearestInteractable(this, scheduler);
        var selectTarget= new ChooseTarget(this, targetManager);
        var moveToSelected = new ReachTarget(this, navMeshAgent, animator, scheduler);
        var interact = new Interact(this, animator, scheduler);
        var die = new Die(this, navMeshAgent, animator, spawnPos);
        var moveForward= new MoveForward(this);
        var wait= new Wait(animator, navMeshAgent);

        At(selectTarget, moveToSelected, HasTarget());
        At(selectTarget, findInteractable, () => task != "Walk");
        At(findInteractable, moveToSelected,
            //HasTarget()
            ()=>true
            );
        //At(moveToSelected, selectTarget, TargetReached());
        //At(selectTarget, findInteractable, () => isInteractive&& !hasInteracted);
        At(moveToSelected, interact, PlayAnimation());
        At(interact, selectTarget, Walk());
        At(interact, findInteractable, NextTask());

        At(moveToSelected, moveForward, InteractableIsFree());
        At(moveToSelected, wait, InteractableBusy());
        //At(wait, moveForward, InteractableFreed());
        At(moveForward, moveToSelected, ()=>true);
        At(wait, moveToSelected, InteractableFreed());

        At(moveToSelected, selectTarget, () => maxQueueCount > 1);      //se 2 maxqueue, cambio task 
        //se maxqueue, cambio interactable
        At(moveToSelected, findInteractable, () => maxQueue);
        At(findInteractable, selectTarget, () => maxQueueCount > 1);    //se maxqueue e non ci sono fratelli interactable, cambio task

        _stateMachine.AddAnyTransition(die, () => isToRemove);
        At(die, selectTarget, SpawnReached());

        _stateMachine.SetState(selectTarget);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> HasTarget() => () => Target != null && (task == "Walk" || task == prevTask);
        //Func<bool> TargetReached() => () => Target != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!isInteractive);
        Func<bool> PlayAnimation() => () =>
            //Target != null &&
            !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            //&& isInteractive
            //&& task != "Walk"
            //&& Target.parent.GetComponent<EnvInteractable>().interactablesBusy[Target.transform.gameObject] == false
            //&& Target.name != "posto"
            && (Target == targetObject.transform
            || (task == "Walk" || task == prevTask))
        ;
        Func<bool> SpawnReached() => () => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
        Func<bool> NextTask() => () => 
            //hasInteracted && 
            task != prevTask && (fineInteract||prevTask=="Walk");
        Func<bool> Walk() => () => 
            //hasInteracted && 
            (task == prevTask || task=="Walk") && (fineInteract || prevTask == "Walk");

        Func<bool> InteractableIsFree() => () =>
            !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            //&& Target.name == "posto"
            && targetObject.GetComponentInParent<EnvInteractable>().interactablesBusy[targetObject] == false
            && isQueuing == false
            ;
        Func<bool> InteractableFreed() => () =>
            //!navMeshAgent.pathPending &&
            //navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            //&& Target.name == "posto"
            targetObject.GetComponentInParent<EnvInteractable>().interactablesBusy[targetObject] == false
            //&& isQueuing == false
        ;
        Func<bool> InteractableBusy() => () =>
            !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
            //&& Target.name == "posto"
            && (targetObject.GetComponentInParent<EnvInteractable>().interactablesBusy[targetObject] == true
            || isQueuing==true)
        ;
    }

    private void Update() => _stateMachine.Tick();

    public void ShowMood()
    {
        moodSprite = moodVisual.GetComponent<SpriteRenderer>();
        Animator _animator = GetComponent<Animator>();
        switch (mood)
        {
            case 1:
                moodSprite.color=Color.green;
                _animator.SetFloat("Forward", 0.4f);
                break;
            case 2:
                moodSprite.color = Color.white;
                _animator.SetFloat("Forward", 0.4f);
                break;
            case 3:
                moodSprite.color = Color.yellow;
                _animator.SetFloat("Forward", 0.5f);
                break;
            case 4:
                moodSprite.color = Color.red;
                //this.GetComponentInParent<Animator>().SetFloat("Forward", 0.6f );
                _animator.SetFloat("Forward", 0.6f);
                break;
            default:
                break;

        }
    }
}
