using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploration : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject ExploreArea;
    public Teleport Teleport;
    public Outline OutlineWorker;
    public Outline OutlineWheel;
    public Playthings Playthings;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = ExploreArea.transform.position;
        Teleport.TeleportToPoint.Enable();
        Playthings.BareHands();
        //OutlineWorker.enabled = true;
        // OutlineWheel.enabled = true;
    }


    private void OnDisable() {
        //Teleport.TeleportToPoint.Disable();
        OutlineWorker.enabled = false;
        OutlineWheel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Scenario.TimeExplore <= 0) {
            Teleport.TeleportToPoint.Disable();
            Scenario.EnterScene("Climbing", Scenario.Dialogue);
            Debug.Log("<color=red>Timeout</color>");
        }
    }
}
