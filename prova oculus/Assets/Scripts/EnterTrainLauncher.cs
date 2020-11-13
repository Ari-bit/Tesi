using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrainLauncher : MonoBehaviour
{
    public Action<float> myAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter()
    {
        if (myAction != null)
        {
            myAction(1f);
        }
    }
}
