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

    }

    public void Tick()
    {
        foreach (Transform t in _spawnPos)
        {
            _spawnPoints.Add(t.gameObject);
        }
        _target = _spawnPoints
            .OrderBy(s => Vector3.Distance(_avatar.transform.position, s.transform.position))
            .FirstOrDefault().transform;
        _avatar.Target = _target;
        _navMeshAgent.stoppingDistance = 0.5f;
        _navMeshAgent.speed = _animator.GetFloat("Forward");
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_target.position);
        _avatar.isToRemove = false;
    }
    public void OnExit()
    {
        Object.Destroy(_avatar.transform.gameObject);
    }

}
