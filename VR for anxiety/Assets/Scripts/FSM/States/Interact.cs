using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : IState
{
    private readonly Avatar _avatar;
    private readonly Animator _animator;

    public Interact(Avatar avatar, Animator animator)
    {
        _avatar = avatar;
        _animator = animator;
    }

    public void Tick()
    {
        //if(_avatar.Target.parent.name!= "Target Points")
        if (_avatar.Target.parent.GetComponent<EnvInteractable>()!=null)
        {
            EnvInteractable interactable = _avatar.Target.parent.GetComponent<EnvInteractable>();
            //Debug.Log(_avatar.Target.parent);
            interactable.Interact(_animator);
        }
        
    }

    public void OnEnter()
    {
        //_avatar.hasInteracted = true;
    }

    public void OnExit()
    {
        
    }


}
