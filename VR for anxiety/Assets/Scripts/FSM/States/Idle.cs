using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{
    private readonly Animator _animator;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly TargetManager _targetManager;

    private float curSpeed;
    private float t;

    public Idle(Animator animator, NavMeshAgent navMeshAgent)
    {
        _animator = animator;
        _navMeshAgent = navMeshAgent;
    }
    public void OnEnter()
    {
        //gestisci il punto occupato?

        //curSpeed = _animator.GetFloat("Forward");
        //t = 0f;
        //_navMeshAgent.isStopped = true;

        //_navMeshAgent.ResetPath();
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        _animator.SetFloat("Forward", 0);
        _navMeshAgent.speed =0;
        _animator.SetTrigger("Idle");

    }

}