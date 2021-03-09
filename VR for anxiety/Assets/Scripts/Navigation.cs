using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Avatar _avatar;
    Coroutine smoothMove = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _avatar = other.GetComponentInParent<Avatar>();
        if ((_avatar.Target == transform.parent)|| (_avatar.Target == transform))
        {
            _navMeshAgent = _avatar.GetComponent<NavMeshAgent>();
            //_avatar.transform.LookAt(_avatar.Target);
            
            StartCoroutine(RotateToDirection(_avatar.transform, _avatar.Target.position, 0.5f));
            _avatar.trigger = true;
            _navMeshAgent.isStopped  = true;
            //_navMeshAgent.velocity= Vector3.zero;
            Debug.Log("trigger target "+ this.name);
            //Selection.activeGameObject = _avatar.gameObject;
            //Debug.Break();
        }

    }
    public IEnumerator RotateToDirection(Transform transform, Vector3 positionToLook, float timeToRotate)
    {
        var startRotation = transform.rotation;
        var direction = positionToLook - transform.position;
        var finalRotation = Quaternion.LookRotation(direction);
        var t = 0f;
        var startVel = _navMeshAgent.velocity;
        while (t <= 1f)
        {
            t += Time.deltaTime / timeToRotate;
            _avatar.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            //_navMeshAgent.velocity= Vector3.Lerp(startVel, Vector3.zero, t);
            yield return null;
        }
        _avatar.transform.rotation = finalRotation;
        _navMeshAgent.velocity = Vector3.zero;

    }
}
