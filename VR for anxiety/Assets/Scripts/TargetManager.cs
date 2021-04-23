using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TargetManager : MonoBehaviour
{
    private int targetIndex;
    private Transform[] targetPoints;        
    private Vector3 spawnPos;
    private int count;
    private AvatarManager initAvatar;
    public bool ready = false;
    [SerializeField] public float radius;
    [SerializeField] public Transform center;
    [SerializeField] private bool manualPositioning= false;
    [SerializeField] private int numberOfPoints;
    [SerializeField] private GameObject targetPointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (manualPositioning==false)
        {
            GenerateTargetPoints();
        }
        else
        {
            GetTargetPoints();
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

    private void GetTargetPoints()
    {
        count = transform.childCount;
        targetPoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            targetPoints[i] = transform.GetChild(i);
        }

        ready = true;
    }

    private void GenerateTargetPoints()    
    {
        //clear manual target points if present
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            transform.DetachChildren();     //perchè non vengono distrutti istantaneamente 
        }

        //generate random points in a hemisphere given radius and center
        //Vector3 pointPosition;
        //for (int i = 0; i < numberOfPoints; i++)
        //{
        //    pointPosition = Random.insideUnitSphere * radius ;
        //    pointPosition.z = Mathf.Abs(pointPosition.z);       //semisfera
        //    pointPosition += center.position;       //rispetto all'user
        //    pointPosition.y = center.position.y;     //sul pavimento     

        //    NavMeshHit hit;
        //    if (NavMesh.SamplePosition(pointPosition, out hit, 2.0f, 1)) //trova il punto sulla navmesh, per gestire gli ostacoli
        //    {
        //        //areamask=1 -> walkable
        //        pointPosition = hit.position;
        //    }
        //    Instantiate(targetPointPrefab, pointPosition,Quaternion.identity, this.transform);
        //}
        for (int i = 0; i < numberOfPoints; i++)
        {
            GenerateIdlePoint();
        }

        GetTargetPoints();
      }

    //just for debug, draw the semicircle
    //si vede solo se targetmanager è selezionato
    [CustomEditor(typeof(TargetManager))]
    public class DrawWireArc : Editor
    {
        void OnSceneGUI()
        {
            Handles.color = new Color(0,0,0,0.2f);
            TargetManager myObj = (TargetManager)target;
            Handles.DrawSolidArc(myObj.center.position, myObj.transform.up, -myObj.transform.right, 180, myObj.radius);
        }
    }

    public GameObject GenerateIdlePoint()    
    {
        //generate random points in a hemisphere given radius and center
        Vector3 pointPosition;
        
        pointPosition = Random.insideUnitSphere * radius ;
        pointPosition.z = Mathf.Abs(pointPosition.z);       //semisfera
        pointPosition += center.position;       //rispetto all'user
        pointPosition.y = center.position.y;     //sul pavimento     

        NavMeshHit hit;
        if (NavMesh.SamplePosition(pointPosition, out hit, 2.0f, 1)) //trova il punto sulla navmesh, per gestire gli ostacoli
        {
            pointPosition = hit.position;
        }
        GameObject g= Instantiate(targetPointPrefab, pointPosition,Quaternion.identity, this.transform);
        return g;
    }
}
