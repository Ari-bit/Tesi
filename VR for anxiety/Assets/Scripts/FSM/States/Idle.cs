using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{
    private readonly Animator _animator;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly NavMeshObstacle _navMeshObstacle;
    private readonly TargetManager _targetManager;

    private float curSpeed;
    private float t;

    public Idle(Animator animator, NavMeshAgent navMeshAgent, NavMeshObstacle navMeshObstacle)
    {
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _navMeshObstacle = navMeshObstacle;
    }
    public void OnEnter()
    {
        //gestisci il punto occupato?

        curSpeed = _animator.GetFloat("Forward");
        t = 0f;
        _navMeshAgent.isStopped = true;

        _navMeshAgent.ResetPath();
        _animator.SetTrigger("Idle");
        _navMeshAgent.enabled = false;
        _navMeshObstacle.enabled = true;        //gli avatar fermi diventano ostacoli modificando la navmesh, così da non essere urtati dagli altri avatar

    }

    public void OnExit()
    {
        _navMeshObstacle.enabled=false;
        _navMeshAgent.enabled = true;
    }

    public void Tick()
    {
        //_animator.SetFloat("Forward", 0);
        //_navMeshAgent.speed =0;
        //_animator.SetTrigger("Idle");
        _animator.SetFloat("Forward", Mathf.Lerp(curSpeed, 0f, t));
        _navMeshAgent.speed = Mathf.Lerp(curSpeed, 0f, t);
        t += 0.8f * Time.deltaTime;
    }

}