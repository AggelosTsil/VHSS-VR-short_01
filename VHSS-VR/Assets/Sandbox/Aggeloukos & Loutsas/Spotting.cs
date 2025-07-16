using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotting : MonoBehaviour {
    public GameObject player;
    public Scenario Scenario;
    public GameObject SpottingArea;
    
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        player.transform.position = SpottingArea.transform.position;
    }
    // Update is called once per frame
    void Update() {

        //<<update timer>>
        Scenario.TimeSpot -= Time.deltaTime;

        if (Scenario.Timer) {
            if (Scenario.TimeSpot <= 0) {
                Scenario.EnterScene("Ending", Scenario.Dialogue);
            }
        }
    }
}
