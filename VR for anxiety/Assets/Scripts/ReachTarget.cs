using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Avatar))]

public class ReachTarget : MonoBehaviour
{
    public Transform _target;

    private NavMeshAgent _navMeshAgent;
    public TargetManager targetManager;
    private Animator _animator;
    private Avatar _avatar;

    private bool hasInteracted = false;

    void Start()
    {
        _avatar = GetComponent<Avatar>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.stoppingDistance = 0.5f;
        _navMeshAgent.speed = _animator.GetFloat("Forward");

        if (_target == null)
        {
            _target = targetManager.SetTarget();
        }
            
        //if (_target != null)
        //  _target.SetActive(false);
    }


    void Update()
    {
        if (_target != null )
        {
            _navMeshAgent.SetDestination(_target.position);
            TargetReached();
        }

        //if (_target != null)
        //    _target.SetActive(!TargetReached());
    }

    private void TargetReached()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                //if (!_navMeshAgent.hasPath)
                //{
                if (_avatar.isInteractive == true && hasInteracted==false)
                {

                    _target.parent.GetComponent<EnvInteractable>().Interact(_animator);
                    //_target.gameObject.GetComponent<EnvInteractable>().Interact(_animator);
                    hasInteracted = true;
                    //hasInteracted= _avatar.Interact();
                    
                }
                _target = targetManager.SetTarget();
  
                //}
            }
        }
    }

}