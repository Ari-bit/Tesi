using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractablesManager : MonoBehaviour
{
    private List<string> tasks= new List<string>();
    private Dictionary<string, EnvInteractable>  envInteractables = new Dictionary<string, EnvInteractable>();

    // Start is called before the first frame update
    void Start()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            string name = transform.GetChild(i).name;
            tasks.Add(name);
            envInteractables.Add(name, transform.GetChild(i).GetComponent<EnvInteractable>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetTasks()
    {
        return tasks;
    }

    public string GetNextTask(Avatar avatar)
    {
        //questo è lo scheduler
        //string nextTask= tasks[Random.Range(0, tasks.Count)];
        //if (nextTask != "Walk" && envInteractables[nextTask].isRepeatible == false && avatar.NRFinishedTasks.Contains(nextTask))
        //{
        //    GetNextTask(avatar);
        //}

        //tutto questo giro per non modificare tasks
        String[] copiaArray = tasks.ToArray();
        List<string> copia = copiaArray.ToList();
        //non posso scegliare i task non ripetibili già eseguiti 1 volta
        for(int i = 0; i < avatar.NRFinishedTasks.Count; i++)
        {
            copia.Remove(avatar.NRFinishedTasks[i]);
        }
        string nextTask = copia[Random.Range(0, copia.Count)];
        return nextTask;
    }
    public bool IsTaskRepeatable(string task)
    {
        return envInteractables[task].isRepeatible;
    }
    /*
    public Transform SelectTarget( string taskName)
    {
        GameObject taskEmpty= GameObject.Find(this.name + "/" + taskName);
        int count = taskEmpty.transform.childCount;
        int index = Random.Range(0, count);
        return taskEmpty.transform.GetChild(index);
    }
    */
}
