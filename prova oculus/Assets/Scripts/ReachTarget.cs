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
        //if (Input.GetMouseButtonDown(0))
        if (_animator.GetBool("OpenDoor")==true)
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
                    return true;

        return false;
    }

}
