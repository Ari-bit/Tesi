using System;
using UnityEngine;
using UnityEngine.AI;

public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject moodVisual;
    private GameObject targetManagerObj;
    public bool isInteractive =false;
    public bool isToRemove = false;
    public bool hasInteracted = false;
    public string task;
    public int mood;    //scala da 1 a 4 dove 1 è un mood positivo e 4 è negativo
    public Transform spawnPos;
    private SpriteRenderer moodSprite;

    private TargetManager targetManager;
    private StateMachine _stateMachine;

    public Transform Target;
    //public EnvInteractable Target { get; set; }

    private void Awake()
    {
        targetManagerObj = GameObject.Find("Target Points");
        targetManager = targetManagerObj.GetComponent<TargetManager>();


        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();

        _stateMachine = new StateMachine();

        var findInteractable= new FindNearestInteractable(this);
        var selectTarget= new ChooseTarget(this, targetManager);
        var moveToSelected = new ReachTarget(this, navMeshAgent, animator);
        var interact = new Interact(this, animator);

        At(selectTarget, moveToSelected, HasTarget());
        At(findInteractable, moveToSelected, HasTarget());
        At(moveToSelected, selectTarget, TargetReached());
        At(selectTarget, findInteractable, () => isInteractive);
        At(moveToSelected, interact, PlayAnimation());
        At(interact, selectTarget, ()=>hasInteracted==true);

        _stateMachine.SetState(selectTarget);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> TargetReached() => () => Target != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!isInteractive|| hasInteracted==true);
        Func<bool> PlayAnimation() => () =>
            Target != null && !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && isInteractive && !hasInteracted;
    }

    private void Update() => _stateMachine.Tick();

    public void ShowMood()
    {
        moodSprite = moodVisual.GetComponent<SpriteRenderer>();
        switch (mood)
        {
            case 1:
                moodSprite.color=Color.green;
                break;
            case 2:
                moodSprite.color = Color.white;
                break;
            case 3:
                moodSprite.color = Color.yellow;
                break;
            case 4:
                moodSprite.color = Color.red;
                this.GetComponentInParent<Animator>().SetFloat("Forward", 0.6f );
                break;
            default:
                break;

        }
    }
}
