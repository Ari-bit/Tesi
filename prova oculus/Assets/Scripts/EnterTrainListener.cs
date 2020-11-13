using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrainListener : MonoBehaviour
{
     Animator _animator;
     public GameObject actionLauncherGO;
     private EnterTrainLauncher launcher;

     // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        launcher = actionLauncherGO.GetComponent<EnterTrainLauncher>();
        launcher.myAction += OnActionReceived;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnActionReceived(float val)
    {
        _animator.SetTrigger("OpenDoor");

    }
}
