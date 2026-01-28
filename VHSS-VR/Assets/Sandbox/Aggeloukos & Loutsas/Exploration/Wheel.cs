using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour 
{
    public GameObject player;
    public float CallHelperTime;
    public Transform pistol;
    public Scenario Scenario;
    public GameObject WheelArea;
    public Playthings Playthings;
    public GameObject HotspotRing;
    public GameObject Aux;
    public Teleport Teleport;
    public GameObject gunR;//gunR and gunL are to check if the player has activated the pistol
    public GameObject gunL;
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        player.transform.position = WheelArea.transform.position;
        Playthings.Holstered("full");
        HotspotRing.SetActive(false);
        Aux.SetActive(false);

        Scenario.directionHelper.GoalMet();
        Scenario.directionHelper.CleanGoals(); //sets up helper
        Scenario.directionHelper.AddGoal(pistol);
        Scenario.directionHelper.StartTiming(CallHelperTime);
    }
    private void OnDisable()
    {
        if (Scenario.TimeExplore >= 0)
        {
            HotspotRing.SetActive(true);
            Aux.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update() {
        if (Scenario.TimeExplore <= 0) {
            Scenario.EnterScene("Explore", Scenario.Dialogue);
        }
        if (gunL.activeSelf || gunR.activeSelf) //cleans the helper when player has found the pistol
        {
            Scenario.directionHelper.RemoveGoal(pistol);
            Scenario.directionHelper.GoalMet();
        } 
    }
}
