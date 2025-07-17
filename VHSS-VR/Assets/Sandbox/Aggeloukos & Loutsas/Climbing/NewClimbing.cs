using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewClimbing : MonoBehaviour {
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
    public Scenario Scenario;
    public GameObject ClimbingArea;
    public GameObject HS;
    public GameObject SpottingArea;
    public Playthings Playthings;

    // Start is called before the first frame update
    private void Start() {
        FailPoint = Player.position;
        Debug.Log("FailPoint set to " + FailPoint);
        Playthings.BareHands();
    }
    void OnEnable() //Void OnEnable is a bit better blahblahblah-optimisation-blahblahblah
    {

        Player.transform.position = ClimbingArea.transform.position;
        HS.SetActive(false);

        //<<initialising positions>>
        CurrentRHPosition = RPositionAction.action.ReadValue<Vector3>();
        CurrentLHPosition = LPositionAction.action.ReadValue<Vector3>();
        FailPoint = Player.position;
        Debug.Log("FailPoint set to " + FailPoint);

        //<<Timer>>
        HangingCheckTimer = HangingCheck;
    }
    private void OnDisable() {
        
    }


    public void Fall(Vector3 FailPoint) {
        //Vector3 FallingPoint = Player.position - FailPoint;
        //Player.Translate(FailPoint);
        Debug.Log("Falling to " + FailPoint + "from" + Player.position);
        Player.position = FailPoint;
    }

    public void GrabMove(bool Hand) { //true is right and false is left
        Debug.Log("GrabMove");
        if (Hand) {
            if (GrabRight.IsPressed()) {
                TravelPoint.Set(-CurrentRHPosition.x + PreviousRHPosition.x, -CurrentRHPosition.y + PreviousRHPosition.y, -CurrentRHPosition.z + PreviousRHPosition.z);
                Debug.Log("<color=red>right hand climbing</color>");
                Player.Translate(TravelPoint * sensitivity, local ? Space.Self : Space.World);
            }
        }
        else {
            if (GrabLeft.IsPressed()) {
                TravelPoint.Set(-CurrentLHPosition.x + PreviousLHPosition.x, -CurrentLHPosition.y + PreviousLHPosition.y, -CurrentLHPosition.z + PreviousLHPosition.z);
                Debug.Log("<color=red>left hand climbing</color>");
                Player.Translate(TravelPoint * sensitivity, local ? Space.Self : Space.World);
            }

        }
    }

    // Update is called once per frame
    void Update() {
        //<<Get position values>>
        PreviousRHPosition = CurrentRHPosition;
        PreviousLHPosition = CurrentLHPosition;
        CurrentRHPosition = RPositionAction.action.ReadValue<Vector3>();
        CurrentLHPosition = LPositionAction.action.ReadValue<Vector3>();

        //<<Update Timer>>
        HangingCheckTimer -= Time.deltaTime;
        Scenario.TimeClimb -= Time.deltaTime;

        //<<Restarting Timer>>
        if (HangingCheckTimer <= 0) {
            HangingCheckTimer = HangingCheck;
            FailPoint = Player.position; //Marks current point as failpoint, meaning player will keep falling here if not holding on
            Debug.Log("FailPoint set to " + FailPoint);
        }


        //Time check
    
        if (Scenario.Timer) {
            if (Scenario.TimeClimb <= 0) {
                Player.transform.position = SpottingArea.transform.position;

                Debug.Log("<color=red>Timeout</color>");
                Scenario.EnterScene("Spotting", Scenario.Dialogue);
            }
        }


    }


}