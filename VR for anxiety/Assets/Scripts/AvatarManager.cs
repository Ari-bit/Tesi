using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] public GameObject tpoints;
    private Avatar[] avatars;

    // Start is called before the first frame update
    void Start()
    {
        TargetManager tmanager = tpoints.GetComponent<TargetManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(GameObject avatar)
    {
        Animator animator = avatar.GetComponent<Animator>();
        animator.SetFloat("Forward", 0.4f);
        avatar.AddComponent<ReachTarget>();
        ReachTarget reach = avatar.GetComponent<ReachTarget>();
        reach.targetManager = tpoints.GetComponent<TargetManager>();
    }
}