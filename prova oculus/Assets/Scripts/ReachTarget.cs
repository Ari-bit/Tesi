using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ReachTarget : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private NavMeshAgent _navMeshAgent;
    Animator _animator;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        if (_target != null)
            _target.SetActive(false);
    }


    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            _navMeshAgent.SetDestination(_target.transform.position);
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
                    _animator.SetTrigger("Sit");
                    float newRotation = _target.transform.rotation.y;
                    transform.Rotate(0f,newRotation,0f);
                    return true;
                //}
            }
        }

        return false;
    }

}
