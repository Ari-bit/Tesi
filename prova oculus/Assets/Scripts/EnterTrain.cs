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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter()
    {
        _animator.SetTrigger("OpenDoor");
        Debug.Log(_animator.parameterCount);
    }
}
