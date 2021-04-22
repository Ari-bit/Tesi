using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTarget : IState
{
    private readonly Avatar _avatar;
    private readonly TargetManager _targetManager;

    public ChooseTarget(Avatar avatar,TargetManager targetManager)
    {
        _avatar = avatar;
        _targetManager = targetManager;
    }

    public void OnEnter()
    {
        _avatar.maxQueueCount = 0;
        _avatar.maxQueue = false;
    }
    public void OnExit() { }

    public void Tick()
    {
        //_avatar.Target = _targetManager.SetTarget();
        //Debug.Break();
        //_avatar.targetObject = _avatar.Target.gameObject;
        if (_targetManager.ready)
        {
            _avatar.task = "Walk";
            //_avatar.targetObject = _targetManager.SetTarget().gameObject;
            _avatar.targetObject = _targetManager.GenerateIdlePoint();

            _avatar.Target = _avatar.targetObject.transform;
        }
        
    }

}