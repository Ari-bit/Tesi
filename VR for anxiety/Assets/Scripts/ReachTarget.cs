using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReachTarget : IState
{
    public Transform _target;
    public Transform _currentTarget;

    private NavMeshAgent _navMeshAgent;
    public TargetManager targetManager;
    private Animator _animator;
    private Avatar _avatar;

    private bool hasInteracted = false;
    private Camera cam;

    private static readonly int Speed = Animator.StringToHash("Forward");
    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;

    public ReachTarget(Avatar avatar, NavMeshAgent navMeshAgent, Animator animator)
    {
        _avatar = avatar;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }
    void Start()
    {
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

    public void Tick()
    {
        if (Vector3.Distance(_avatar.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _avatar.transform.position;
    }

    public void OnEnter()
    {
        TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        //_navMeshAgent.SetDestination(_avatar.Target.transform.position);
        _navMeshAgent.SetDestination(_target.position);
        _animator.SetFloat(Speed, 1f);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetFloat(Speed, 0f);
    }

    //void Update()
    //{
    //    if (_target != null )
    //    {
    //        _navMeshAgent.SetDestination(_target.position);
    //        TargetReached();
    //    }

    //    //if (_target != null)
    //    //    _target.SetActive(!TargetReached());
    //}

    //public void TargetReached()
    //{
    //    if (!_navMeshAgent.pathPending)
    //    {
    //        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
    //        {
    //            //if (!_navMeshAgent.hasPath)
    //            //{
                
    //            if (_avatar.isInteractive  && hasInteracted==false && _avatar.isToRemove==false )
    //            {
    //                EnvInteractable interactable = _target.parent.GetComponent<EnvInteractable>();
    //                if (interactable.interactablesBusy[_target.transform.gameObject] == false)
    //                {
    //                    interactable.Interact(_animator);
    //                    _currentTarget = _target;
    //                }
    //                //_target.gameObject.GetComponent<EnvInteractable>().Interact(_animator);
    //                hasInteracted = true;
    //                _target = targetManager.SetTarget();
    //            }
    //            else if (_avatar.isToRemove==true)
    //            {
    //                //l'avatar da rimuovere ritorna al suo spawnpoint originale e viene distrutto se l'utente non sta guardando.
    //                //se sta guardando, l'avatar va verso il prossimo target e se non è visto viene distrutto 
    //                Spawn spawn= new Spawn();
    //                if (spawn.IsSpawnHidden(_avatar.spawnPos, cam) == true)
    //                {
    //                    Destroy(_avatar.transform.gameObject);
    //                }
    //                else
    //                {
    //                    _target = targetManager.SetTarget();
    //                    _avatar.spawnPos = _target;
    //                }
    //            }
    //            else _target = targetManager.SetTarget();
    //            //}
    //        }
    //    }
    //}

}