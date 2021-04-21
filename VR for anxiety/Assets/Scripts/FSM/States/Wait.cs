using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wait : IState
{
    private readonly Animator _animator;
    private readonly NavMeshAgent _navMeshAgent;
    private float curSpeed;
    private float t;

    public Wait(Animator animator, NavMeshAgent navMeshAgent)
    {
        _animator = animator;
        _navMeshAgent = navMeshAgent;
    }
    public void OnEnter()
    {
        //Debug.Break();
        //_navMeshAgent.isStopped = true;
        //_navMeshAgent.velocity= Vector3.zero;
        //_navMeshAgent.enabled = false;

        //_animator.SetFloat("Forward", 0);
        //_navMeshAgent.speed = _animator.GetFloat("Forward");
        //_navMeshAgent.ResetPath();

        //_animator.SetTrigger("Idle");
        //Debug.Log("Start wait");

        curSpeed = _animator.GetFloat("Forward");
        t = 0f;
        _navMeshAgent.isStopped = true;
        
        _navMeshAgent.ResetPath();
    }

    public void OnExit()
    {
        //SDebug.Log("End wait");
        //Debug.Break();
        //_navMeshAgent.enabled = true;
        //_navMeshAgent.isStopped = false;
        //_navMeshAgent.speed = _animator.GetFloat("Forward");

        //aspettare un istante prima di passare allo stato successivo?
    }

    public void Tick()
    {
        _animator.SetFloat("Forward", Mathf.Lerp(curSpeed, 0f, t));
        _navMeshAgent.speed = Mathf.Lerp(curSpeed, 0f, t);
        t += 0.8f * Time.deltaTime;
    }

}
