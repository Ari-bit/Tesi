using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn 
{
    private Vector3 rayOrigin;
    private Vector3 dirToSpawn;
    private float dstToSpawn;

    private bool _debugRay=false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_debugRay)
            DebugRaycast();
    }

    public bool IsSpawnHidden(Transform spawnPos, Camera cam)
    {
        Vector3 viewPos = cam.WorldToViewportPoint(spawnPos.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            dirToSpawn = (spawnPos.position - cam.transform.position).normalized;
            dstToSpawn = Vector3.Distance(cam.transform.position, spawnPos.position);
            rayOrigin = cam.transform.position;
            int layerMask = 1 << 2;
            layerMask = ~layerMask;
            if (Physics.Raycast(rayOrigin, dirToSpawn, dstToSpawn, layerMask))
            {
                Debug.Log("ostacolo davanti a " + spawnPos.name + " , ostacolo: " + Physics.RaycastAll(cam.transform.position, dirToSpawn, dstToSpawn)[0].transform.name);
                return true;
            }
            else
            {
                Debug.Log("sto guardando ");
                return false;
            }
        }
        else return true;
        
    }
    private void DebugRaycast()
    {
        Debug.DrawRay(rayOrigin, dirToSpawn * dstToSpawn, Color.red);
    }
}
