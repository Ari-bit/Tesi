﻿using System.Collections;
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
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = targetManager.SetTarget();
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
                return true;
                //}
            }
        }

        return false;
    }

}