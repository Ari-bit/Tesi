﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuspendNavigation : StateMachineBehaviour
{
    private NavMeshAgent _navMeshAgent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetNavMeshAgent(animator).isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetNavMeshAgent(animator).isStopped = false;
    }

    public NavMeshAgent GetNavMeshAgent(Animator animator)
    {
        if (_navMeshAgent == null)
        {
            _navMeshAgent = animator.GetComponentInParent<NavMeshAgent>();
        }
        return _navMeshAgent;
    }
}
