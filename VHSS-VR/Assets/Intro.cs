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
        if (!Scenario.SeagullSpeaking.isPlaying)
        {
            Scenario.EnterScene("Explore", Scenario.Dialogue);
            gun.SetActive(true);
        }
    }
}
