using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ReachTarget : MonoBehaviour
{
    private Transform _target;

    private NavMeshAgent _navMeshAgent;
    public TargetManager targetManager;
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.stoppingDistance = 0.5f;
        _navMeshAgent.speed = _animator.GetFloat("Forward");
        
        _target = targetManager.SetTarget();
        Debug.Log("Start , "+ _target);
        //if (_target != null)
        //  _target.SetActive(false);
    }


    void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
            TargetReached();
        }

        //if (_target != null)
        //    _target.SetActive(!TargetReached());
    }

    private bool TargetReached()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                //if (!_navMeshAgent.hasPath)
                //{
                    _target= targetManager.SetTarget();
                    Debug.Log( "Reach , " + _target);
                    return true;
                //}
            }
        }

        return false;
    }

}