using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] public GameObject tpoints;
    [SerializeField] public GameObject interactables;
    public List<Avatar> avatars = new List<Avatar>();
    
    private List<string> tasks;
    private int taskIndex;
    private InteractablesManager _imanager;

    // Start is called before the first frame update
    void Start()
    {
        TargetManager tmanager = tpoints.GetComponent<TargetManager>();
        _imanager = interactables.GetComponent<InteractablesManager>();
        tasks = _imanager.GetTasks();
        tasks.Add("Base");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(GameObject avatarObj, Transform spawnPos)
    {
        Animator animator = avatarObj.GetComponent<Animator>();
        animator.SetFloat("Forward", 0.4f);

        avatarObj.AddComponent<ReachTarget>();
        ReachTarget reach = avatarObj.GetComponent<ReachTarget>();
        reach.targetManager = tpoints.GetComponent<TargetManager>();

        Avatar avatar = avatarObj.GetComponent<Avatar>();
        avatars.Add(avatar);
        taskIndex = Random.Range(0, tasks.Count);
        avatar.task = tasks[taskIndex];
        avatar.spawnPos = spawnPos;
        avatar.mood = Random.Range(1, 5);
        avatar.ShowMood();

        if (avatar.task!= "Base")
        {
            avatar.isInteractive = true;
            //reach._target = GameObject.Find(interactables.name+"/"+avatar.task).transform;
            reach._target = _imanager.SelectTarget(avatar.task);
            //GameObject.Find(avatar.task).GetComponent<EnvInteractable>();
            //Debug.Log(reach._target);
        }
        //avatars.Append(avatar);
    }

    public void RemoveAvatar()
    {
        Avatar[] avatarArray = avatars.ToArray();
        Avatar avatarToRemove = avatarArray[0];
        avatarToRemove.isToRemove = true;
        ReachTarget reach = avatarToRemove.GetComponent<ReachTarget>();
        reach._target = avatarToRemove.spawnPos;
        avatars.Remove(avatarArray[0]);
    }
}