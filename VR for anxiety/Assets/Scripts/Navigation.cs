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
            _navMeshAgent.stoppingDistance = 1.5f;      //to change state
            _avatar.trigger = true;
            //Debug.Break();
            _navMeshAgent.isStopped  = true;
            //_avatar.GetComponentInParent<Animator>().SetFloat("Forward", 0f);
            _navMeshAgent.speed = 0f;
            _navMeshAgent.ResetPath();
            //_navMeshAgent.velocity= Vector3.zero;
            //Debug.Log("trigger target "+ this.name);
            //Selection.activeGameObject = _avatar.gameObject;
            //Debug.Break();
            //Debug.Log(_navMeshAgent.pathEndPosition+ " "+ _navMeshAgent.destination);
        }

    }
    public IEnumerator RotateToDirection(Transform transform, Vector3 positionToLook, float timeToRotate)
    {
        var startRotation = transform.rotation;
        var direction = positionToLook - transform.position;
        direction.y = 0f;           //only rotate on y axis
        var finalRotation = Quaternion.LookRotation(direction);
        var t = 0f;
        var startVel = _navMeshAgent.velocity;
        var curSpeed =_avatar.GetComponentInParent<Animator>().GetFloat("Forward");

        while (t <= 1f)
        {
            t += Time.deltaTime / timeToRotate;
            _avatar.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, t);
            //_navMeshAgent.velocity= Vector3.Lerp(startVel, Vector3.zero, t);
            _avatar.GetComponentInParent<Animator>().SetFloat("Forward", curSpeed-t/2);

            yield return null;
        }
        _avatar.transform.rotation = finalRotation;
        //_navMeshAgent.velocity = Vector3.zero;
        //_avatar.GetComponentInParent<Animator>().SetFloat("Forward", 0f);

    }
}
