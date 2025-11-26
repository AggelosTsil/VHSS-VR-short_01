using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject IntroArea;
    public Playthings Playthings;
    public GameObject gun;

    public float Time; //don't touch it is perfectly timed 
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = IntroArea.transform.position;
        Playthings.BareHands(); 
        gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = IntroArea.transform.position;
        if (Scenario.TimeExplore <= 120 - Time)
        {
            Scenario.EnterScene("Explore", Scenario.Dialogue);
            gun.SetActive(true);
        }
    }
}
