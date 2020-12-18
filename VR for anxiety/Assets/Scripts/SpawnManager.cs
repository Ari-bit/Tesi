using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    [SerializeField] private AvatarManager avatarManager;
    //[SerializeField] private int MAX_AVATAR;
    [SerializeField] private SlidersUI slider;

    private int spawnIndex;
    private Transform spawnPos;
    private Transform[] spawnpoints;
    private int spawnCount;
    private Camera cam;
    private int avatarCount =0;

    private int MAX_AVATAR;

    //private Vector3 rayOrigin;
    //private Vector3 dirToSpawn;
    //private float dstToSpawn;

    void Start()
    {
        cam= Camera.main;

        spawnCount = transform.childCount;
        spawnpoints = new Transform[spawnCount];
        for (int i = 0; i < spawnCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i);
        }

        
        //InvokeRepeating("spawnAvatars", 1, 5);
        //spawnAvatars();
    }

    void Update()
    {
        MAX_AVATAR = slider.GetAvatarCount();
        if (avatarCount < MAX_AVATAR && !IsInvoking("spawnAvatars"))
        {
            InvokeRepeating("spawnAvatars", 1, 5);
        }
        else if (avatarCount == MAX_AVATAR)
        {
            CancelInvoke();
        }
        else if (avatarCount > MAX_AVATAR)
        {
            while (avatarCount!= MAX_AVATAR)
            {
                avatarManager.RemoveAvatar();
                avatarCount--;
            }
        }

    }

    void spawnAvatars()
    {
        spawnIndex = Random.Range(0, spawnCount);
        spawnPos = spawnpoints[spawnIndex];
        Spawn spawn = new Spawn();
        if (spawn.IsSpawnHidden(spawnPos, cam)==true)
        //if (IsSpawnHidden() == true)
        {
            GameObject avatar = Instantiate(avatarPrefab, spawnpoints[spawnIndex].position, avatarPrefab.transform.rotation, transform.parent);
            avatarManager.Init(avatar, spawnpoints);
            avatarCount++;
        }
    }

    //public bool IsSpawnHidden()
    //{
    //    Vector3 viewPos = cam.WorldToViewportPoint(spawnPos.position);
    //    if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
    //    {
    //        dirToSpawn = (spawnPos.position - cam.transform.position).normalized;
    //        dstToSpawn = Vector3.Distance(cam.transform.position, spawnPos.position);
    //        rayOrigin = cam.transform.position;
    //        if (Physics.Raycast(rayOrigin, dirToSpawn, dstToSpawn))
    //        {
    //            Debug.Log("ostacolo davanti a " + spawnPos.name + " , ostacolo: " + Physics.RaycastAll(cam.transform.position, dirToSpawn, dstToSpawn)[0].transform.name);
    //            return true;
    //        }
    //        else
    //        {
    //            Debug.Log("sto guardando " + spawnpoints[spawnIndex].name);
    //            return false;
    //        }
    //    }
    //    else return true;

    //}


}
