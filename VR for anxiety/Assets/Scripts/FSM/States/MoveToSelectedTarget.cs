using UnityEngine;
using UnityEngine.AI;

internal class MoveToSelectedTarget : IState
{
    private readonly Avatar _avatar;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 _lastPosition = Vector3.zero;
    
    public float TimeStuck;

    public MoveToSelectedTarget(Avatar avatar, NavMeshAgent navMeshAgent, Animator animator)
    {
        _avatar = avatar;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }
    
    public void Tick()
    {
        if (Vector3.Distance(_avatar.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _avatar.transform.position;
    }

    public void OnEnter()
    {
        TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_avatar.Target.transform.position);
        _animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetFloat(Speed, 0f);
    }
}