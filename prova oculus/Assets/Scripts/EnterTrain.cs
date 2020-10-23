using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrain : MonoBehaviour
{
     Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter()
    {
        Debug.Log("arrivato");
        _animator.SetTrigger("OpenDoor");
    }
}
