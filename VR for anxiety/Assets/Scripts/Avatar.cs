using System;
using UnityEngine;
using UnityEngine.AI;

public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject moodVisual; 
    public bool isInteractive =false;
    public bool isToRemove = false;
    public string task;
    public int mood;    //scala da 1 a 4 dove 1 è un mood positivo e 4 è negativo
    public Transform spawnPos;
    private SpriteRenderer moodSprite;

    private StateMachine _stateMachine;
    //public EnvInteractable Target { get; set; }

    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();

        _stateMachine = new StateMachine();

        //var search = new SearchForInteractable(this);
        var moveToSelected = new ReachTarget(this, navMeshAgent, animator);
        //var interact = new Interact(this, animator);

        //At(moveToSelected, interact, TargetReached());
        _stateMachine.SetState(moveToSelected);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> TargetReached() => () => moveToSelected._target != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
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
