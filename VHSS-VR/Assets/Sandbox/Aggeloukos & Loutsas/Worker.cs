using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject WorkerArea;
    public Playthings Playthings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        player.transform.position = WorkerArea.transform.position;
        Playthings.BareHands();
    }

    // Update is called once per frame
    void Update()
    {
        if (Scenario.TimeExplore <= 0) {
            Scenario.EnterScene("Climbing", Scenario.Dialogue);
        }
    }
}
