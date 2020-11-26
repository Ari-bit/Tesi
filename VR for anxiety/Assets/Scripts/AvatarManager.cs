using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] public GameObject tpoints;

    // Start is called before the first frame update
    void Start()
    {
        TargetManager tmanager = tpoints.GetComponent<TargetManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(GameObject avatar, RuntimeAnimatorController controller)
    {
        Animator animator = avatar.GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        animator.SetFloat("Forward", 0.4f);
        avatar.AddComponent<ReachTarget>();
        ReachTarget reach = avatar.GetComponent<ReachTarget>();
        reach.targetManager = tpoints.GetComponent<TargetManager>();
    }
}