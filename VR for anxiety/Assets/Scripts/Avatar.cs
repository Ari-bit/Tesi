using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private GameObject moodVisual; 
    public bool isInteractive =false;
    public bool isToRemove = false;
    public string task;
    public int mood;    //scala da 1 a 4 dove 1 è un mood positivo e 4 è negativo
    public Transform spawnPos;
    private SpriteRenderer moodSprite;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMood()
    {
        moodSprite = moodVisual.GetComponent<SpriteRenderer>();
        switch (mood)
        {
            case 1:
                moodSprite.color=Color.green;
                break;
            case 2:
                moodSprite.color = Color.white;
                break;
            case 3:
                moodSprite.color = Color.yellow;
                break;
            case 4:
                moodSprite.color = Color.red;
                break;
            default:
                break;

        }
    }
}
