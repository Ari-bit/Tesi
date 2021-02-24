using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReachTarget : IState
{
    public Transform _target;
    public Transform _currentTarget;

    private NavMeshAgent _navMeshAgent;
    public TargetManager _targetManager;
    private Animator _animator;
    private Avatar _avatar;

    private static readonly int Speed = Animator.StringToHash("Forward");
    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;

    private readonly InteractablesManager _imanager;

    public ReachTarget(Avatar avatar, NavMeshAgent navMeshAgent, Animator animator, InteractablesManager scheduler)
    {
        _avatar = avatar;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _imanager = scheduler;
    }


    public void Tick()
    {
        if (Vector3.Distance(_avatar.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _avatar.transform.position;

        _target = _avatar.Target;
        _navMeshAgent.SetDestination(_target.position);

    }

    public void OnEnter()
    {
        //_navMeshAgent.stoppingDistance = 0.6f;

             //prova, da cambiare con la vel di ogni avatar
        _avatar.ShowMood();
        _navMeshAgent.speed = _animator.GetFloat("Forward");

        _navMeshAgent.stoppingDistance = _animator.GetFloat("Forward") + 0.2f;

        //if (_target == null)
        //{
        /*_target = _avatar.Target;*/
        //_target = _targetManager.SetTarget();
        //}
        TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        //_navMeshAgent.SetDestination(_avatar.Target.transform.position);

        /*_navMeshAgent.SetDestination(_target.position);*/

        //_animator.SetFloat(Speed, 0.4f);
    }

    public void OnExit()
    {
        //_navMeshAgent.enabled = false;
        //_animator.SetFloat(Speed, 0f);

        //_avatar.prevTask = _avatar.task;
        //if (_avatar.prevTask != "Walk"&&_imanager.IsTaskRepeatable(_avatar.prevTask) == false)
        //{
        //    _avatar.NRFinishedTasks.Add(_avatar.prevTask);
        //}
        //_avatar.task = _imanager.GetNextTask(_avatar);
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