using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : IState
{
    private readonly Avatar _avatar;
    private readonly Animator _animator;
    private EnvInteractable i;

    public Interact(Avatar avatar, Animator animator)
    {
        _avatar = avatar;
        _animator = animator;
    }

    public void Tick()
    {
        //if(_avatar.Target.parent.name!= "Target Points")
        //if (_avatar.targetObject.transform.parent.GetComponent<EnvInteractable>()!=null)
        //{
        //    EnvInteractable interactable = _avatar.targetObject.transform.parent.GetComponent<EnvInteractable>();
        //    //Debug.Log(_avatar.Target.parent);
        //    interactable.Interact(_animator);
        //}
        
    }

    public void OnEnter()
    {
        //_avatar.hasInteracted = true;
        if (_avatar.targetObject.transform.parent.GetComponent<EnvInteractable>() != null)
        {
            i = _avatar.targetObject.transform.parent.GetComponent<EnvInteractable>();
            i.interactablesBusy[_avatar.targetObject] = true;
            i.Interact(_animator);
        }
    }

    public void OnExit()
    {
        if (_avatar.targetObject.transform.parent.GetComponent<EnvInteractable>() != null)
            i.interactablesBusy[_avatar.targetObject] = false;
        //if (_avatar.Target.parent.GetComponent<EnvInteractable>() != null)
        //{
        //    _avatar.Target.parent.GetComponent<EnvInteractable>().interactablesBusy[_avatar.targetObject] = false;
        //}
    }


}
