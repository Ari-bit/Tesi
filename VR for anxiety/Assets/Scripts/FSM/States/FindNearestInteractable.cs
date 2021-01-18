using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindNearestInteractable : IState
{
    private readonly Avatar _avatar;
    private readonly InteractablesManager _imanager;
    //private List<GameObject> tasksObj = new List<GameObject>();

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
        List<GameObject> tasksObj = new List<GameObject>();
        GameObject taskEmpty = GameObject.Find("Interactables" + "/" + _avatar.task);
        //Debug.Log(taskEmpty);
        int count = taskEmpty.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            tasksObj.Add(taskEmpty.transform.GetChild(i).gameObject);
        }
        return tasksObj
            .OrderBy(t => Vector3.Distance(_avatar.transform.position, t.transform.position))
            .FirstOrDefault().transform;
    }
    

    public void OnEnter()
    {
        
    }
    public void OnExit()
    {
        //_avatar.prevTask = _avatar.task;
        //_avatar.task = _imanager.GetNextTask();
    }
}
