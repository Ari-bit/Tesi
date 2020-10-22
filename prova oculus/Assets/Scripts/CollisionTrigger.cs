using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider avatar)
    {
        _animator.SetTrigger("Bump");
    }
}
