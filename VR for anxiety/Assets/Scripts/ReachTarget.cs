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
    public Transform _currentTarget;

    private NavMeshAgent _navMeshAgent;
    public TargetManager targetManager;
    private Animator _animator;
    private Avatar _avatar;

    private bool hasInteracted = false;
    private Camera cam;

    void Start()
    {
        _avatar = GetComponent<Avatar>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.stoppingDistance = 0.5f;
        _navMeshAgent.speed = _animator.GetFloat("Forward");

        cam= Camera.main;

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

    public void TargetReached()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                //if (!_navMeshAgent.hasPath)
                //{
                
                if (_avatar.isInteractive  && hasInteracted==false && _avatar.isToRemove==false )
                {
                    EnvInteractable interactable = _target.parent.GetComponent<EnvInteractable>();
                    if (interactable.objects[_target.transform.gameObject] == false)
                    {
                        interactable.Interact(_animator);
                        _currentTarget = _target;
                    }
                    //_target.gameObject.GetComponent<EnvInteractable>().Interact(_animator);
                    hasInteracted = true;
                    //hasInteracted= _avatar.Interact();
                    _target = targetManager.SetTarget();
                }
                else if (_avatar.isToRemove==true)
                {
                    //l'avatar da rimuovere ritorna al suo spawnpoint originale e viene distrutto se l'utente non sta guardando.
                    //se sta guardando, l'avatar va verso il prossimo target e se non è visto viene distrutto 
                    Spawn spawn= new Spawn();
                    if (spawn.IsSpawnHidden(_avatar.spawnPos, cam) == true)
                    {
                        Destroy(_avatar.transform.gameObject);
                    }
                    else
                    {
                        _target = targetManager.SetTarget();
                        _avatar.spawnPos = _target;
                    }
                }
                else _target = targetManager.SetTarget();
                //}
            }
        }
    }

}