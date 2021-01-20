using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : IState
{
    private readonly Avatar _avatar;

    public MoveForward(Avatar avatar)
    {
        _avatar = avatar;
    }
    public void OnEnter()
    {
        _avatar.Target = _avatar.targetObject.transform;
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }
}
