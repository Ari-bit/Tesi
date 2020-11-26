using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class ProvaBottone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Premuto(InteractableStateArgs obj)
    {
        if (obj.NewInteractableState == InteractableState.ActionState)
        {
            Debug.Log("premuto");
        }
    }
}
