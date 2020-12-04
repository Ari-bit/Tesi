using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractable : EnvInteractable
{
   
    // Start is called before the first frame update
    void Start()
    {
        count = transform.childCount;
        interactablesBusy = new Dictionary<GameObject, bool>();
        for (int i = 0; i < count; i++)
        {
            interactablesBusy.Add(transform.GetChild(i).gameObject, false);
        }
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
