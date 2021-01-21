using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wait : IState
{
    private readonly Animator _animator;
    private readonly NavMeshAgent _navMeshAgent;

    public Wait(Animator animator, NavMeshAgent navMeshAgent)
    {
        _animator = animator;
        _navMeshAgent = navMeshAgent;
    }
    public void OnEnter()
    {
        //_navMeshAgent.isStopped = true;
        //_navMeshAgent.enabled = false;
        _animator.SetTrigger("Idle");
        //Debug.Break();
    }

    public void OnExit()
    {
        //_navMeshAgent.enabled = true;
        //_navMeshAgent.isStopped = false;
        //_navMeshAgent.speed = _animator.GetFloat("Forward");

    }

    public void Tick()
    {

    }
}
