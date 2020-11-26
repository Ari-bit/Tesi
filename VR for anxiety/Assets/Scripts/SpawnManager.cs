﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    private int spawnIndex;
    private Transform[] spawnpoints;
    private Vector3 spawnPos;
    private int count;
    [SerializeField] private AvatarManager initAvatar;
    private Camera cam;

    void Start()
    {
        cam= Camera.main;

        count = transform.childCount;
        spawnpoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            spawnpoints[i] = transform.GetChild(i);
        }

        InvokeRepeating("spawnAvatars", 1, 5);
        //spawnAvatars();
    }

    void spawnAvatars()
    {
        
        spawnIndex = Random.Range(0, count);
        Vector3 viewPos = cam.WorldToViewportPoint(spawnpoints[spawnIndex].position);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            Debug.Log("sto guardando " + spawnIndex);
        }
        else
        {
            GameObject avatar = Instantiate(avatarPrefab, spawnpoints[spawnIndex].position, avatarPrefab.transform.rotation, transform.parent);
            //initAvatar = new InitializeAvatar();
            initAvatar.Init(avatar);
        }
            
        
    }
}
