using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    private int spawnIndex;
    private Transform[] spawnpoints;
    private Vector3 spawnPos;
    private int spawnCount;
    [SerializeField] private AvatarManager initAvatar;
    private Camera cam;
    public int MAX_AVATAR;
    private int avatarCount =0;

    void Start()
    {
        cam= Camera.main;

        spawnCount = transform.childCount;
        spawnpoints = new Transform[spawnCount];
        for (int i = 0; i < spawnCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i);
        }

        InvokeRepeating("spawnAvatars", 1, 5);
        
        //spawnAvatars();
    }

    // Update is called once per frame
    void Update()
    {
        if (avatarCount == MAX_AVATAR)
        {
            CancelInvoke();
        }
    }

    void spawnAvatars()
    {
        
        spawnIndex = Random.Range(0, spawnCount);
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
            avatarCount++;
        }
            
        
    }
}
