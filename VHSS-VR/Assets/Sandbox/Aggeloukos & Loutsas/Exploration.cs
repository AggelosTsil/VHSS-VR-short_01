using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploration : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject ExploreArea;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = ExploreArea.transform.position;
    }

    public void TeleportToHotSpot(GameObject Anchor) {
        if  (Anchor.tag == "Activity Area") {
            if (Anchor.name == "Worker Area") {
                Scenario.EnterScene("Worker", Scenario.Dialogue);
            }
            else {
                Scenario.EnterScene("Wheel", Scenario.Dialogue);
            }
        }
    }

    private void RevealHotSpot(GameObject HotSpot) {

    }

    // Update is called once per frame
    void Update()
    {
        if (Scenario.TimeExplore <= 0) {
            Scenario.EnterScene("Climbing", Scenario.Dialogue);
            Debug.Log("<color=red>Timeout</color>");
        }
    }
}
