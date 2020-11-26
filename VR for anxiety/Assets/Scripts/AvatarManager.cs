using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Init(GameObject avatarObj)
    {
        Animator animator = avatarObj.GetComponent<Animator>();
        animator.SetFloat("Forward", 0.4f);
        avatarObj.AddComponent<ReachTarget>();
        ReachTarget reach = avatarObj.GetComponent<ReachTarget>();
        reach.targetManager = tpoints.GetComponent<TargetManager>();

        Avatar avatar = avatarObj.GetComponent<Avatar>();
        //avatars.Append(avatar);
    }
}