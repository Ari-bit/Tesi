using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QueueManager : MonoBehaviour
{
    //private List<Transform> _queuePoints;
    private Transform[] _queuePoints;

    public Queue<Avatar> _avatarQueue;
    private int _maxQueue;
    private int queued = 0;

    void Start()
    {
        //_queuePoints = new List<Transform>();
        _maxQueue = transform.parent.GetComponent<EnvInteractable>().maxQueue;
        _queuePoints = new Transform[_maxQueue];
        _avatarQueue = new Queue<Avatar>();

        _queuePoints[0] = transform;
        for(int i=1; i<_maxQueue; i++)
        {
            //da creare da codice poi
            //_queuePoints.Add(transform.GetChild(i+1));
            _queuePoints[i]=transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (queued != 0)
        //{
        //    transform.parent.GetComponent<EnvInteractable>().interactablesBusy[this.gameObject] = true;
        //}
        //queued = _avatarQueue.Count;
        //vedere se un avatar sta arrivando (controllare avatar.targetObj?)
        //assegbarlo ad un punto della fila (cambiargli il target, inserirlo in queue)
    }

    private void OnTriggerEnter(Collider other)
    {
        Avatar avatar= other.GetComponentInParent<Avatar>();
        if(avatar.targetObject== transform.gameObject)
        {
            if (queued == _maxQueue)
            {
                //Debug.Log("MAXQUEUE");
                //Selection.activeGameObject = avatar.gameObject;
                //Debug.Break();

                //int index = transform.GetSiblingIndex();
                //int siblingIndex=0;
                //for(int i=0; i<transform.parent.childCount; i++)
                //{
                //    if (i != index)
                //    {
                //        siblingIndex = i;
                //        break;
                //    }
                //}
                ////avatar.Target = transform.parent.GetChild(siblingIndex);
                ////avatar.targetObject = avatar.Target.gameObject;
                //avatar.targetObject = transform.parent.GetChild(siblingIndex).gameObject;       //non basarsi sulla struttura gerarchica 

                //avatar.Target = transform.parent.GetChild(siblingIndex).GetChild(0);
                avatar.exclude = transform;
                avatar.maxQueue = true;
                avatar.maxQueueCount++;
                //Debug.Log("count: "+avatar.maxQueueCount);
            }
            else
            {
                //Debug.Log("sta andando a ticket");  
                _avatarQueue.Enqueue(avatar);
                transform.GetComponentInParent<EnvInteractable>().interactablesBusy[this.gameObject] = true;    //segno come occupato prima di arrivarci
                avatar.InteractionCompleted += OnAvatarInteractionCompleted;    //il manager si iscrive all'evento per registrare quando l'avatar finisce l'interazione
                                                                                // per poi toglierlo dalla fila (piuttosto che aspettare che esca dalla zona di trigger)

                queued = _avatarQueue.Count;
                avatar.Target = _queuePoints[queued - 1];
                //Debug.Log(avatar.Target.name);
                //Selection.activeGameObject = avatar.gameObject;
                //Debug.Break();
                avatar.maxQueue = false;

                avatar.maxQueueCount=0;
                if (queued > 1)
                {
                    avatar.isQueuing = true;
                }
            }
            
        }
    }

    private void OnAvatarInteractionCompleted(Avatar avatar) 
    {
        avatar.InteractionCompleted -= OnAvatarInteractionCompleted;      //il manager smette di "ascoltare" l'avatar che ha scatenato l'evento
        avatar.isQueuing = false;
        if (_avatarQueue.Contains(avatar))
        {
            _avatarQueue.Dequeue();
            queued = _avatarQueue.Count;

            for (int i = 0; i < queued; i++)
            {
                _avatarQueue.ToArray()[i].Target = _queuePoints[i];
            }
        }
    }
}
