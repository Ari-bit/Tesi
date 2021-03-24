using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Die : IState
{
    private readonly Avatar _avatar;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly Transform[] _spawnPos;
    private readonly List<GameObject> _spawnPoints = new List<GameObject>();
    private Transform _target;

    public Die (Avatar avatar, NavMeshAgent navMeshAgent, Animator animator, Transform[] spawnPos)
    {
        _avatar = avatar;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _spawnPos = spawnPos;
        
    }

    public void OnEnter()
    {
        foreach (Transform t in _spawnPos)
        {
            _spawnPoints.Add(t.gameObject);
        }
    }

    public void Tick()
    {
        _target = _spawnPoints
            .OrderBy(s => Vector3.Distance(_avatar.transform.position, s.transform.position))
            .FirstOrDefault().transform;
        _avatar.Target = _target;

        //prende i valori iniziali
        _animator.SetFloat("Forward", _avatar.speed);
        _navMeshAgent.stoppingDistance = _avatar.speed + 0.2f;
        _navMeshAgent.speed = _avatar.speed;

        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_target.position);
        _avatar.isToRemove = false;

        if (_avatar.InteractionCompleted != null)
        {
            _avatar.InteractionCompleted(_avatar);      //per svuotare la queue
        }
    }

    public void OnExit()
    {
        Spawn spawn = new Spawn();
        if (spawn.IsSpawnHidden(_target, Camera.main) == true)
        {
            Object.Destroy(_avatar.transform.gameObject);
            _avatar.findAnotherPoint = false;
        }
        else
        {
            _avatar.findAnotherPoint = true;
        }
    }
}
