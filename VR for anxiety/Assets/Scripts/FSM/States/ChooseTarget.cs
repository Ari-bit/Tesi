﻿using System.Collections;
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

    public void OnEnter() { }
    public void OnExit() { }

    public void Tick()
    {
        _avatar.Target = _targetManager.SetTarget();
        _avatar.targetObject = _avatar.Target.gameObject;
    }

}