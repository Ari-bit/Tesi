using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SliderMood : MonoBehaviour
{

    private float moodValue = 0.0f;
    private float avatarCount = 10;

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 130, 50), "Mood Controller");

        moodValue = GUI.HorizontalSlider(new Rect(25, 40, 100, 30), moodValue, -3.0f, 3.0f);

        GUI.Box(new Rect(10, 60, 130, 50), "Avatar Volume: "+ (int)Math.Round(avatarCount));
        avatarCount = (GUI.HorizontalSlider(new Rect(25, 90, 100, 30), avatarCount, 0, 40));
    }

    public int GetAvatarCount()
    {
        return (int)Math.Round(avatarCount);
    }

}
