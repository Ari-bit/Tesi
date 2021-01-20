using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//generica classe task

public class EnvInteractable : MonoBehaviour
{
    public Dictionary<GameObject, bool> interactablesBusy;    //per tenere traccia degli oggetti interactable se sono occupati o meno
    public int count;
    public bool isRepeatible;

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact(Animator _animator)
    {

    }
}
