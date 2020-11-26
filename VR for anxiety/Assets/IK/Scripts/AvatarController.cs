using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    [SerializeField] private List<Transform> pickuppableObjects = new List<Transform>(); //only for the example, grabbable objects don't need to be stored here
    [SerializeField] private Transform rightHandTransform; //necessary to parent the grabbed object to the avatar's hand (not implemented in this example)
    private float rightHandIKWeight = 0.0f;
    [SerializeField] private bool rightHandIKActive = true;
    private Transform rightHandIKTarget;
    [SerializeField] private Vector3 pickupOffset; //this vector determines the offset of the center of the hand in respect to the object, an alternative is to tie a "grabtarget" empty object to each grabbable, and set that as a grab target
    private Animator animator;
    private bool isPickingUp;
    private float wH;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //OnAnimatorIK() is called automatically at every frame, like update. No need to be explicitly called
    private void OnAnimatorIK()
    {
        //if the IK is active, set the position and rotation directly to the goal. 
        if (rightHandIKActive)
        {

            wH = animator.GetIKPositionWeight(AvatarIKGoal.RightHand);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandIKWeight);
            //rotation is not used in this example, enable to also align the hand to the object IF NEEDED
            //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandIKWeight);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position + pickupOffset);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);

            animator.SetLookAtWeight(1.0f * rightHandIKWeight, 0.25f, 1.0f, 1.0f, 0.0f); //this slightly rotates the head and body towards the grab target, for simplicity it uses the same weight of the hand, however it could be assigned to a different variable
            animator.SetLookAtPosition(rightHandIKTarget.position);

        }

        //if the IK is not active, set the position and rotation of the head back to the original position
        else
        {
            animator.SetLookAtWeight(0);
        }
    }

    public void PickupEvent()
    {
        //do stuff here when the avatar picks up the target object, for example parent object to the hand
        //rightHandIKTarget.transform.parent = rightHandTransform;
        isPickingUp = false;
    }

    private void UpdateAnimator()
    {
        animator.SetBool("pickUp", isPickingUp);
    }



    //------------MOST IMPORTANT FUNCTION---------------
    public void Grab(Transform objectToGrab) //call this function to grab an object, public so that it can be called from outside
    {
        rightHandIKTarget = objectToGrab;
        rightHandIKActive = true;
        isPickingUp = true;
    }
    //------------MOST IMPORTANT FUNCTION--------------



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!rightHandIKTarget) //this check is needed to make sure that only one grab animation is fired at any time
            {
                //randomly select IK target between available pickuppable objects, then fires the Grab function
                Transform randomPickupObject = pickuppableObjects[Random.Range(0, pickuppableObjects.Count)];
                Grab(randomPickupObject);
            }
        }


        //interpolation of IK pass weight
        if (rightHandIKActive)
        {
            if (isPickingUp && wH < 1.0f)
            {
                rightHandIKWeight += 0.5f * Time.deltaTime;
            }

            if (!isPickingUp && rightHandIKWeight > 0.0f)
            {
                rightHandIKWeight -= 0.5f * Time.deltaTime;
                if (rightHandIKWeight <= 0.0f)
                {
                    rightHandIKWeight = 0.0f;
                    rightHandIKActive = false;
                    rightHandIKTarget = null;
                }
            }
        }

        //updates animator variables (only one in the example) so that everything is always synchronized
        UpdateAnimator();
    }
}
