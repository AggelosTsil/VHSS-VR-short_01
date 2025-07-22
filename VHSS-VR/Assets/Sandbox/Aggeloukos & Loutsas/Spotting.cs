using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotting : MonoBehaviour {
    public GameObject player;
    public Scenario Scenario;
    public GameObject SpottingArea;
    public Playthings Playthings;
    public HandGrabRope GrabbingRight;
    public HandGrabRope GrabbingLeft;
    public GameObject[] HotspotRing;
    public GameObject[] Aux;

    public bool IsSpotting;
    
    // Start is called before the first frame update
    void Start() {
        Playthings.PistolActive(false);
        Playthings.SpyglassActive(true);
        GrabbingRight.enabled = false;
        GrabbingLeft.enabled = false;

    }

    private void OnEnable() {
        IsSpotting = true;
        HotspotRing[0].SetActive(false);
        Aux[0].SetActive(false);
        HotspotRing[1].SetActive(false);
        Aux[1].SetActive(false);
    }

    private void OnDisable(){
        IsSpotting = false;
        HotspotRing[0].SetActive(true);
        Aux[0].SetActive(true);
        HotspotRing[1].SetActive(true);
        Aux[1].SetActive(true);
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
        /*if (!Scenario.SeagullSpeaking.isPlaying) {
            if (Playthings.Toggle.triggered) {
                Debug.Log("Toggle Playthings");
                Playthings.PistolActive(!Playthings.Pistol.active);
                Playthings.SpyglassActive(!Playthings.Spyglass.active);

            }
        }*/
    }
}
