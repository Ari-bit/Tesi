using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{

    private float waitTime = 60.0f;
    private float timer = 0.0f;
    [SerializeField] private int many = 50;
    [SerializeField] private int few = 10;
    [SerializeField] private SlidersUI slider;

    void Update()
    {
        timer += Time.deltaTime;

       if (timer > waitTime)
        {
            //evento
            timer = timer - waitTime;   //azzero
            if(slider.GetAvatarCount()==few)
                slider.SetAvatarCount(many);
            else 
                slider.SetAvatarCount(few);
        }
    }
    
}

