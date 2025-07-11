using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
    public GameObject player;
    public Scenario Scenario;
    public GameObject WheelArea;
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        player.transform.position = WheelArea.transform.position;
    }
    // Update is called once per frame
    void Update() {
        if (Scenario.TimeExplore <= 0) {
            Scenario.EnterScene("Climbing", Scenario.Dialogue);
        }
    }
}
