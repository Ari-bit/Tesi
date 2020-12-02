using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    [SerializeField] private AvatarManager initAvatar;
    [SerializeField] private int MAX_AVATAR;
    [SerializeField] private bool _debugRay;

    private int spawnIndex;
    private Transform[] spawnpoints;
    private Vector3 spawnPos;
    private int spawnCount;
    private Camera cam;
    private int avatarCount =0;

    private Vector3 rayOrigin;
    private Vector3 dirToSpawn;
    private float dstToSpawn;

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
        if (_debugRay)
            DebugRaycast();
    }

    void spawnAvatars()
    {
        
        spawnIndex = Random.Range(0, spawnCount);
        Vector3 viewPos = cam.WorldToViewportPoint(spawnpoints[spawnIndex].position);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            dirToSpawn = (spawnpoints[spawnIndex].position - cam.transform.position).normalized;
            dstToSpawn= Vector3.Distance(cam.transform.position, spawnpoints[spawnIndex].position);
            rayOrigin = cam.transform.position;
            if (Physics.Raycast(rayOrigin, dirToSpawn, dstToSpawn))
            {
                Debug.Log("ostacolo davanti a " + spawnpoints[spawnIndex].name + " , ostacolo: "+Physics.RaycastAll(cam.transform.position, dirToSpawn, dstToSpawn)[0].transform.name);
                GameObject avatar = Instantiate(avatarPrefab, spawnpoints[spawnIndex].position, avatarPrefab.transform.rotation, transform.parent);
                initAvatar.Init(avatar);
                avatarCount++;
            }
            else
            {
                Debug.Log("sto guardando " + spawnpoints[spawnIndex].name);
            }
        }
        else
        {
            GameObject avatar = Instantiate(avatarPrefab, spawnpoints[spawnIndex].position, avatarPrefab.transform.rotation, transform.parent);
            //initAvatar = new InitializeAvatar();
            initAvatar.Init(avatar);
            avatarCount++;
        }


        //if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //{
        //    Debug.Log("sto guardando " + spawnpoints[spawnIndex].name);
        //}
        //else
        //{
        //    GameObject avatar = Instantiate(avatarPrefab, spawnpoints[spawnIndex].position, avatarPrefab.transform.rotation, transform.parent);
        //    //initAvatar = new InitializeAvatar();
        //    initAvatar.Init(avatar);
        //    avatarCount++;
        //}
    }
    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, dirToSpawn * dstToSpawn, Color.red);
    }
}
