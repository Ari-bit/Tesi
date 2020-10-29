using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrain : MonoBehaviour
{
     Animator _animator;

     [SerializeField] private GameObject avatar;
    // Start is called before the first frame update
    void Start()
    {
        _animator = avatar.GetComponent<Animator>();
        _animator.SetBool("doorsOpened", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter()
    {
        Debug.Log("arrivato");
        _animator.SetTrigger("OpenDoor");
       // _animator.SetBool("doorsOpened", true);
        Debug.Log(_animator.parameterCount);
    }
}
