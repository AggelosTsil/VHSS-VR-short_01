using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    public GameObject player;
    public Scenario Scenario;
    public GameObject EndingArea;
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        player.transform.position = EndingArea.transform.position;
    }
    // Update is called once per frame
    void Update() {
        
    }
}
