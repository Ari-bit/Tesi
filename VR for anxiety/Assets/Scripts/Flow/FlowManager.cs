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
    [SerializeField] private SpawnManager spawnM;
    private bool minutaggio = false;

    void Start()
    {
        spawnM.sogliaRaggiunta += OnSogliaRaggiunta;
    }

    void Update()
    {
        timer += Time.deltaTime;

       if (timer > waitTime && minutaggio)
        {
            spawnM.sogliaRaggiunta += OnSogliaRaggiunta;        //mi riiscrivo all'evento

            timer = timer - waitTime;   //azzero
            slider.SetVisualTime(timer);
            //if (slider.GetAvatarCount()==few)       //da non prendere dallo slider
            if (spawnM.MAX_AVATAR == few)      
                slider.SetAvatarCount(many);
                //arrivo del treno
            else 
                slider.SetAvatarCount(few);
            minutaggio = false;
        }
    }

    private void OnSogliaRaggiunta(int soglia)
    {
        spawnM.sogliaRaggiunta -= OnSogliaRaggiunta;
        Debug.Log("soglia raggiunta " +soglia);
        timer = 0;   //azzero, da qui deve partire 1 min
        slider.SetVisualTime(timer);
        minutaggio = true;
    }
}

