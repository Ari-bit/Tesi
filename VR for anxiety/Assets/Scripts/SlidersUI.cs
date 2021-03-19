using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SlidersUI : MonoBehaviour
{

    private float moodValue = 0.0f;
    private float avatarCount = 40;
    private float controllerRadius = 0.5f;

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 130, 50), "Mood Controller");

        moodValue = GUI.HorizontalSlider(new Rect(25, 40, 100, 30), moodValue, -3.0f, 3.0f);

        GUI.Box(new Rect(10, 60, 130, 50), "Avatar Volume: "+ (int)Math.Round(avatarCount));
        avatarCount = (GUI.HorizontalSlider(new Rect(25, 90, 100, 30), avatarCount, 0, 40));

        GUI.Box(new Rect(10, 110, 130, 50), "Controller Radius: " + controllerRadius);
        controllerRadius = (GUI.HorizontalSlider(new Rect(25, 140, 100, 30), controllerRadius, 0.5f, 4));

        GUI.Box(new Rect(Screen.width-130-10, 10, 130, 50), "Time Controller: " + Time.timeScale);
        Time.timeScale = GUI.HorizontalSlider(new Rect(Screen.width - 100- 25, 40, 100, 30), Time.timeScale, 1f, 8f);
    }

    public int GetAvatarCount()
    {
        return (int)Math.Round(avatarCount);
    }
    public float GetControllerRadius()
    {
        return controllerRadius;
    }

}
