﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketInteractable : EnvInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        isRepeatible = true;   //dev'essere false
        interactablesBusy = new Dictionary<GameObject, bool>();
        maxQueue = 4;  //era 4
        busy= new bool[2];
        count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            interactablesBusy.Add(transform.GetChild(i).gameObject, false);
            transform.GetChild(i).gameObject.AddComponent<QueueManager>();

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
        interactablesBusy.Values.CopyTo(busy, 0);   //FOR DEBUGGING
    }
    public override void Interact(Animator _animator)
    {
        _animator.SetTrigger("PickUp");
    }
}
