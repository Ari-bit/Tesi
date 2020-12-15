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
    private TargetManager _tmanager;

    // Start is called before the first frame update
    void Start()
    {
        _tmanager = tpoints.GetComponent<TargetManager>();
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

        //avatarObj.AddComponent<ReachTarget>();
        //ReachTarget reach = avatarObj.GetComponent<ReachTarget>();
        //reach.targetManager = tpoints.GetComponent<TargetManager>();

        Avatar avatar = avatarObj.GetComponent<Avatar>();
        avatars.Add(avatar);
        taskIndex = Random.Range(0, tasks.Count);
        avatar.task = tasks[taskIndex];
        avatar.spawnPos = spawnPos;
        //avatar.targetManager = _tmanager;
        avatar.mood = Random.Range(1, 5);
        avatar.ShowMood();

        if (avatar.task!= "Base")
        {
            avatar.isInteractive = true;
            //avatar.Target = _imanager.SelectTarget(avatar.task);
        }
    }

    public void RemoveAvatar()
    {
        Avatar[] avatarArray = avatars.ToArray();
        //l'avatar da rimuovere è il più vecchio generato
        Avatar avatarToRemove = avatarArray[0];
        avatarToRemove.isToRemove = true;
        ReachTarget reach = avatarToRemove.GetComponent<ReachTarget>();
        //il suo target diventa lo spawn point da cui è nato
        reach._target = avatarToRemove.spawnPos;
        avatars.Remove(avatarArray[0]);
    }
}