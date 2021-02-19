using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindNearestInteractable : IState
{
    private readonly Avatar _avatar;
    private readonly InteractablesManager _imanager;
    //private List<GameObject> tasksObj = new List<GameObject>();
    EnvInteractable interactable;

    public FindNearestInteractable(Avatar avatar, InteractablesManager scheduler)
    {
        _avatar = avatar;
        _imanager = scheduler;

    }

    public void Tick()
    {
       _avatar.Target = ChooseTheNearestInteractable();
    }

    private Transform ChooseTheNearestInteractable()
    {
        List<GameObject> tasksObjs = new List<GameObject>();
        GameObject taskEmpty = GameObject.Find("Interactables" + "/" + _avatar.task);
        //Debug.Log(taskEmpty);
        int count = taskEmpty.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if(taskEmpty.transform.GetChild(i)!= _avatar.exclude)       //escludi il max queue
                tasksObjs.Add(taskEmpty.transform.GetChild(i).gameObject);
        }
        //se lista vuota -> non ci sono fratelli -> cambia task
        if (tasksObjs.Count == 0)
        {
            _avatar.task = "Walk";
            _avatar.fineInteract = true;
        }
        //
        GameObject taskObj = tasksObjs
            .OrderBy(t => Vector3.Distance(_avatar.transform.position, t.transform.position))
            .FirstOrDefault();

        _avatar.targetObject = taskObj;
        //if (taskObj.transform.childCount != 0)
        //{
        //    EnvInteractable interactable = taskObj.transform.parent.GetComponent<EnvInteractable>();
        //    taskObj = interactable.interactablesBusy.Keys.FirstOrDefault(t => interactable.interactablesBusy[t] == false);
        //    interactable.interactablesBusy[taskObj] = true;
        //}
        interactable = taskObj.transform.parent.GetComponent<EnvInteractable>();
        //if (interactable.interactablesBusy[taskObj] == true)
        //{
        //    GameObject row = new GameObject("posto");
        //    row.transform.parent = taskObj.transform;
        //    row.transform.rotation = taskObj.transform.rotation;
        //    row.transform.position = taskObj.transform.position + 3 * Vector3.right; //verso la x
        //    taskObj = row;
        //}
        if (taskObj.transform.childCount != 0)
        {
            taskObj = taskObj.transform.GetChild(0).gameObject;     //punto di controllo
        }

        //return tasksObjs
        //    .OrderBy(t => Vector3.Distance(_avatar.transform.position, t.transform.position))
        //    .FirstOrDefault().transform;
        return taskObj.transform;
}
    

    public void OnEnter()
    {
    }
    public void OnExit()
    {
        _avatar.maxQueue = false;
        _avatar.exclude = null;

        //_avatar.prevTask = _avatar.task;
        //_avatar.task = _imanager.GetNextTask();
    }
}
