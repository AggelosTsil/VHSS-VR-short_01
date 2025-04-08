using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class WheelRotationControlsNEW : MonoBehaviour
{
    //Right Hand
    public GameObject righthand;
    public Transform righthandT;
    public bool RightOnWheel = false;
    //Left hand
    public GameObject lefthand;
    public Transform lefthandT;
    public bool LeftOnWheel = false;
    //handles
    public Transform[] SnapPositions;
    //the player i think
    private Transform originalParent;
    private int NumberOfHandsOnWheel = 0;
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OVRInput.Get(OVRInput.RawButton.A));
        ReleaseHandsFromWheel();

        ConvertHandRotationToSteeringWheelRotation();

        //currentSteeringWheel = -transform.rotation.eulerAngles.z;
    }
    void ConvertHandRotationToSteeringWheelRotation() //7:52 sto video mporei na mhn mas noiazei
    {

    }
    void ReleaseHandsFromWheel()
    {
        if (RightOnWheel == true && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            righthand.transform.parent = righthandT;
            righthand.transform.position = righthandT.position;
            righthand.transform.rotation = righthandT.rotation;
            RightOnWheel = false;
            NumberOfHandsOnWheel--;
        }
        if (LeftOnWheel == true && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            lefthand.transform.parent = lefthandT;
            lefthand.transform.position = lefthandT.position;
            lefthand.transform.rotation = lefthandT.rotation;
            LeftOnWheel = false;
            NumberOfHandsOnWheel--;
        }
        if (LeftOnWheel == false && RightOnWheel == false)
        {
            //resets wheel ????? whatever that means 
            transform.parent = transform.root;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (RightOnWheel == false && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                Debug.Log("yay right");
                PlaceHandOnWheel(ref righthand, ref righthandT, ref RightOnWheel);
            }
            if (LeftOnWheel == false && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                Debug.Log("yay left");
                PlaceHandOnWheel(ref lefthand, ref lefthandT, ref LeftOnWheel);
            }
        }
    }
    private void PlaceHandOnWheel(ref GameObject hand, ref Transform original, ref bool HandOnWheel)
    {
        var shortestDistance = Vector3.Distance(SnapPositions[0].position, hand.transform.position);
        var bestSnapp = SnapPositions[0];

        foreach (var snappPosition in SnapPositions)
        {
            if (snappPosition.childCount == 0)
            {
                var distance = Vector3.Distance(snappPosition.position, hand.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnapp = snappPosition;
                }
            }
        }
        originalParent = hand.transform.parent;

        hand.transform.parent = bestSnapp.transform;
        hand.transform.position = bestSnapp.transform.position;

        HandOnWheel = true;
        NumberOfHandsOnWheel++;
    }
}
