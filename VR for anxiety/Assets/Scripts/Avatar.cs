using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public bool isInteractive =false;
    public bool isToRemove = false;
    public string task;
    private string mood;
    public Transform spawnPos;

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

    //public bool Interact()
    //{
    //    _animator.SetTrigger("PickUp");
    //    return true;
    //}
}
