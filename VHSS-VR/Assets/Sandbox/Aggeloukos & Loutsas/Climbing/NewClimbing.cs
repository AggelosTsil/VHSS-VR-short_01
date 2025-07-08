using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewClimbing : MonoBehaviour
{
    //<<Actions>>
    public InputAction GrabRight;
    public InputAction GrabLeft;
    public InputActionReference RPositionAction;
    public InputActionReference LPositionAction;
 
    //<<Right hand position>>
    private Vector3 CurrentRHPosition;
    private Vector3 PreviousRHPosition;

    //<<Left hand position>>
    private Vector3 CurrentLHPosition;
    private Vector3 PreviousLHPosition;

    //<<Object references>>
    public Transform Player;
    public Transform RightHand;
    public Transform Lefthand;

    //<<Misc>>
    private Vector3 TravelPoint; //used to move the player
    public float sensitivity; //Affects climbing speed,change accordingly
    public bool local; //I dont remember honestly 
    public float HangingCheck; //This ticks down and checks if one of the hands is grabbing the ropes on 0
    public float HangingCheckTimer;//Current state of HangingCheck
    private Vector3 FailPoint;//The point where the player will drop if they are not hanging on
    public float FallSensitivity;

    // Start is called before the first frame update
    private void Start() {
        FailPoint = Player.position;
        Debug.Log("FailPoint set to " + FailPoint);
    }
    void OnEnable() //Void OnEnable is a bit better blahblahblah-optimisation-blahblahblah
    {
        //<<Enabling grab actions>>  THIS WILL CHANGE TO ENABLE ONLY ON CONTACT WITH THE ROPES
        GrabRight.Enable();
        GrabLeft.Enable();

        //<<initialising positions>>
        CurrentRHPosition = RPositionAction.action.ReadValue<Vector3>();
        CurrentLHPosition = LPositionAction.action.ReadValue<Vector3>();
        FailPoint = Player.position;
        Debug.Log("FailPoint set to " + FailPoint);

        //<<Timer>>
        HangingCheckTimer = HangingCheck;
    }

    public void Fall(Vector3 FailPoint) {
        //Vector3 FallingPoint = Player.position - FailPoint;
        //Player.Translate(FailPoint);
        Debug.Log("Falling to " + FailPoint + "from" + Player.position);
        Player.position = Vector3.Lerp(Player.position, FailPoint, Time.deltaTime * FallSensitivity);
    }

    public void GrabMove(bool Hand) { //true is right and false is left
        if (Hand) {
            TravelPoint.Set(-CurrentRHPosition.x + PreviousRHPosition.x, -CurrentRHPosition.y + PreviousRHPosition.y, -CurrentRHPosition.z + PreviousRHPosition.z);
        }
        else {
            TravelPoint.Set(-CurrentLHPosition.x + PreviousLHPosition.x, -CurrentLHPosition.y + PreviousLHPosition.y, -CurrentLHPosition.z + PreviousLHPosition.z);
        }
        Player.Translate(TravelPoint * sensitivity, local ? Space.Self : Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        //<<Get position values>>
        PreviousRHPosition = CurrentRHPosition;
        PreviousLHPosition = CurrentLHPosition;
        CurrentRHPosition = RPositionAction.action.ReadValue<Vector3>();
        CurrentLHPosition = LPositionAction.action.ReadValue<Vector3>();

        //<<Update Timer>>
        HangingCheckTimer -= Time.deltaTime;

        //<<Grabbing magic>>
        if (GrabRight.IsPressed()) {
            if (GrabRight.IsPressed() && GrabLeft.IsPressed()) {
                //both hands grabbing
                Debug.Log("<color=green>BOTH HANDS GRABBING</color>");

            }else {
                //RIGHT hand grabbing
                GrabMove(true);
                Debug.Log("<color=yellow>Right hand</color>");
            }

        }else if (GrabLeft.IsPressed()) {
            //LEFT hand grabbing
            GrabMove(false); //dont be confused, GrabMove is active! false is to activate left hand
            Debug.Log("<color=blue>Left hand</color>");

        }else if (HangingCheckTimer <= 0) {
            //Caught lacking! 
            Debug.Log("<color=red>No hands grabbing, falling down :c</color>");
            Fall(FailPoint);
            HangingCheckTimer = HangingCheck;
        }

        //<<Restarting Timer>>
        if (HangingCheckTimer <= 0) {
            HangingCheckTimer = HangingCheck;
            FailPoint = Player.position; //Marks current point as failpoint, meaning player will keep falling here if not holding on
            Debug.Log("FailPoint set to " + FailPoint);
        }


    }


}
