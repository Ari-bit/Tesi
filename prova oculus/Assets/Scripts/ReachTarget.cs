using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ReachTarget : MonoBehaviour
{
    [SerializeField] private GameObject _targetFeedback;

    private NavMeshAgent _navMeshAgent;
    Animator _animator;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        if (_targetFeedback != null)
            _targetFeedback.SetActive(false);
    }


    void Update()
    {
        //if (_animator.GetBool("OpenDoor")==true)
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            _navMeshAgent.SetDestination(_targetFeedback.transform.position);
        }

        if (_targetFeedback != null)
            _targetFeedback.SetActive(!TargetReached());
    }

    private bool TargetReached()
    {
        if (!_navMeshAgent.pathPending)
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    _animator.SetTrigger("Sit");
                    return true;

        return false;
    }

}
