using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistanceManager : MonoBehaviour
{
    [SerializeField] private SlidersUI slider;
    private NavMeshObstacle navMeshObstacle;
    private float radius;
    private float radiusPrec;

    // Start is called before the first frame update
    void Start()
    {
        navMeshObstacle = this.GetComponentInParent<NavMeshObstacle>();
        radiusPrec= slider.GetControllerRadius();
        navMeshObstacle.carving = false;
    }

    // Update is called once per frame
    void Update()
    {
        radius = slider.GetControllerRadius();
        if (radius!= radiusPrec)
        {
            SetRadius();
        }
        
    }

    private void SetRadius()
    {
        navMeshObstacle.radius = radius;
    }
}
