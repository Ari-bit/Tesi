using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvInteractable : MonoBehaviour
{
    public Dictionary<GameObject, bool> objects;
    public int count;

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact(Animator _animator)
    {

    }
}
