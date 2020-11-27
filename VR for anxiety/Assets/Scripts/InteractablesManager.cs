using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    private List<string> tasks= new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            tasks.Add(transform.GetChild(i).name);
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

    public Transform SelectTarget( string taskName)
    {
        GameObject taskEmpty= GameObject.Find(this.name + "/" + taskName);
        int count = taskEmpty.transform.childCount;
        int index = Random.Range(0, count);
        return taskEmpty.transform.GetChild(index);
    }
}
