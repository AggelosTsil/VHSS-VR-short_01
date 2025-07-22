using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject WorkerArea;
    public Playthings Playthings;
    public GameObject HotspotRing;
    public GameObject Aux;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        player.transform.position = WorkerArea.transform.position;
        Playthings.BareHands();
        HotspotRing.SetActive(false);
        Aux.SetActive(false);
    }

    private void OnDisable() {
        HotspotRing.SetActive(true);
        Aux.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Scenario.TimeExplore <= 0) {
            Scenario.EnterScene("Explore", Scenario.Dialogue);
        }
    }
}
