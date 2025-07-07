using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewClimbing : MonoBehaviour
{
    //<<Actions>>
    public InputAction GrabRight;
    public InputAction GrabLeft;
    public InputAction MoveRight;
    public InputAction MoveLeft;
    //<<Player Position>>
    private Vector3 CurrentBodyPosition;
    private Vector3 PreviousBodyPosition;
    //<<Right hand position>>
    private Vector3 CurrentRHPosition;
    private Vector3 PreviousRHPosition;
    //<<Left hand position>>
    private Vector3 CurrentLHPosition;
    private Vector3 PreviousLHPosition;
    //<<Object references>>
    public Transform Body;
    public Transform RightHand;
    public Transform Lefthand;
    //<<Misc>>
    private Vector3 TravelPoint; //used to move the player
    public float sensitivity; //Affects climbing speed,change accordingly
    public bool local; //I dont remember honestly 
    public float HangingCheckTimer; //This ticks down and checks if one of the hands is grabbing the ropes on 0

    // Start is called before the first frame update
    private void Start() {
        
    }
    void OnEnable() //Void OnEnable is a bit better blahblahblah-optimisation-blahblahblah
    {
        //<<Enabling grab actions>>  THIS WILL CHANGE TO ENABLE ONLY ON CONTACT WITH THE ROPES
        GrabRight.Enable();
        GrabLeft.Enable();

        //<<initialising positions>>
        CurrentBodyPosition = Body.position;
        CurrentRHPosition = RightHand.position;
        CurrentLHPosition = Lefthand.position;
            
    }

    // Update is called once per frame
    void Update()
    {
        //<<Get position values>>
        CurrentBodyPosition = Body.position;
        PreviousRHPosition = CurrentRHPosition;
        PreviousLHPosition = CurrentLHPosition;
        CurrentRHPosition = RightHand.position;
        CurrentLHPosition = Lefthand.position;

        //<<Update Timer>>
        HangingCheckTimer -= Time.deltaTime;

        //<<Grabbing magic>> !!!!!!LOGIC ISNT VALID AND NEEDS WORK BUT CODE IS FUNCTIONAL!!!!!!
        if (GrabRight.IsPressed()) {
            if (GrabRight.IsPressed() && GrabLeft.IsPressed()) {
                //both hands grabbing
                Debug.Log("<color=green>BOTH HANDS GRABBING</color>");
            }else {
                //RIGHT hand grabbing
                TravelPoint.Set(CurrentRHPosition.x - PreviousRHPosition.x, -CurrentRHPosition.y + PreviousRHPosition.y, -CurrentRHPosition.z + PreviousRHPosition.z);
                Debug.Log("<color=yellow>Right hand</color>");
            }
        }else if (GrabLeft.IsPressed()) {
            //LEFT hand grabbing
            TravelPoint.Set(CurrentLHPosition.x - PreviousLHPosition.x, -CurrentLHPosition.y + PreviousLHPosition.y, -CurrentLHPosition.z + PreviousLHPosition.z);
            Debug.Log("<color=blue>Left hand</color>");
        }else if (HangingCheckTimer <= 0) {
            //Caught lacking! 
            Debug.Log("<color=red>No hands grabbing, falling down :c</color>");
        }
        //<<Refreshing positions>>
        Body.Translate(TravelPoint * sensitivity, local ? Space.Self : Space.World); 


    }


}
