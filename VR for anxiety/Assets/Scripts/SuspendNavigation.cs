using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuspendNavigation : StateMachineBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private EnvInteractable interactable;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetNavMeshAgent(animator).isStopped = true;
        //GetNavMeshAgent(animator).enabled = false;    //per evitare che pattinino, ma da problemi col rigidbody

        //segno l'interactable su cui viene fatta l'animazione come occupato
        //interactable= animator.GetComponentInParent<ReachTarget>()._currentTarget.transform.gameObject;
        //interactable.GetComponentInParent<EnvInteractable>().interactablesBusy[interactable] = true;

        //interactable = animator.GetComponentInParent<Avatar>().Target.gameObject;
        //interactable.GetComponentInParent<EnvInteractable>().interactablesBusy[interactable] = true;
        animator.GetComponentInParent<Avatar>().fineInteract = false;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        //interactable = animator.GetComponentInParent<ReachTarget>()._currentTarget.transform.gameObject;
        //interactable.transform.parent.GetComponent<EnvInteractable>().objects[interactable] = false;
        //interactable.GetComponentInParent<EnvInteractable>().interactablesBusy[interactable] = false;

        //interactable = animator.GetComponentInParent<Avatar>().Target.parent.gameObject;
        //interactable.GetComponentInParent<EnvInteractable>().interactablesBusy[interactable] = false;

        //Avatar avatar = animator.GetComponentInParent<Avatar>();
        //interactable = avatar.targetObject.GetComponentInParent<EnvInteractable>();
        //interactable.interactablesBusy[avatar.targetObject] = false;
        animator.GetComponentInParent<Avatar>().sequence--;
        if (animator.GetComponentInParent<Avatar>().sequence == 0)
        {
            GetNavMeshAgent(animator).enabled = true;
            GetNavMeshAgent(animator).isStopped = false;
            animator.GetComponentInParent<Avatar>().fineInteract = true;

        }

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
