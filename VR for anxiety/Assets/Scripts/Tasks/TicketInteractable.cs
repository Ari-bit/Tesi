using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketInteractable : EnvInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        isRepeatible = false;
        interactablesBusy = new Dictionary<GameObject, bool>();
        count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            interactablesBusy.Add(transform.GetChild(i).gameObject, false);
        }
        //count = transform.GetChild(0).transform.childCount;
        //for (int i = 0; i < count; i++)
        //{
        //    interactablesBusy.Add(transform.GetChild(0).transform.GetChild(i).gameObject, false);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(Animator _animator)
    {
        _animator.SetTrigger("PickUp");
    }
}
