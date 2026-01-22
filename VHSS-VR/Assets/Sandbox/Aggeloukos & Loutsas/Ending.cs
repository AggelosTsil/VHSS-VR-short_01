using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    public GameObject player;
    public Scenario Scenario;
    public GameObject EndingArea;
    public GameObject ClimbingArrow;
    public GameObject Spotting1;
    public GameObject Spotting2;
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        Destroy(ClimbingArrow);
        Destroy(Spotting1);
        Destroy(Spotting2);
    }
    // Update is called once per frame
    void Update() {
        
    }
}
