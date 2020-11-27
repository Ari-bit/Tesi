using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    private List<string> tasks= new List<string>();
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = transform.childCount;
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
}
