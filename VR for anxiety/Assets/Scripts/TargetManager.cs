using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private int targetIndex;
    private Transform[] targetPoints;
    private Vector3 spawnPos;
    private int count;
    private AvatarManager initAvatar;
    // Start is called before the first frame update
    void Start()
    {
        count = transform.childCount;
        targetPoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            targetPoints[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform SetTarget()
    {
        targetIndex = Random.Range(0, count);
        return targetPoints[targetIndex];
    }
}
