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
    private readonly Avatar _avatar;


    private float curSpeed;
    private float t;
    private float timer;

    public Idle(Animator animator, NavMeshAgent navMeshAgent, NavMeshObstacle navMeshObstacle, Avatar avatar)
    {
        _animator = animator;
        _navMeshAgent = navMeshAgent;
        _navMeshObstacle = navMeshObstacle;
        _avatar = avatar;
    }
    public void OnEnter()
    {
        //gestisci il punto occupato?

        curSpeed = _animator.GetFloat("Forward");
        t = 0f;
        _navMeshAgent.isStopped = true;

        _navMeshAgent.ResetPath();
        _animator.SetTrigger("Idle");

        _navMeshAgent.radius = 0;               //a disabilitare il navmeshagent si avrebbero problemi con il collider
        _navMeshObstacle.enabled = true;        //gli avatar fermi diventano ostacoli modificando la navmesh, così da non essere urtati dagli altri avatar

        timer = 0;
    }

    public void OnExit()
    {
        _navMeshObstacle.enabled = false;
        _navMeshAgent.radius = 0.7f;

    }

    public void Tick()
    {
        //_animator.SetFloat("Forward", 0);
        //_navMeshAgent.speed =0;
        //_animator.SetTrigger("Idle");
        _animator.SetFloat("Forward", Mathf.Lerp(curSpeed, 0f, t));
        _navMeshAgent.speed = Mathf.Lerp(curSpeed, 0f, t);
        t += 0.8f * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > _avatar.idleTime)
        {
            _avatar.idleTimeout = true;
        }
    }

}