using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using System.Linq;

public class Exploration : MonoBehaviour
{
    public GameObject player;
    public Scenario Scenario;
    public GameObject ExploreArea;
    public Teleport TeleportR;
    public Teleport TeleportL;
    public Outline OutlineWorker;
    public Outline OutlineWheel;
    public Playthings Playthings;
    

    public GameObject[] Phase2HotSpots;
    public GameObject[] Phase1HotSpots;
    public GameObject Bill;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = ExploreArea.transform.position;
        TeleportR.TeleportToPoint.Enable();
        TeleportL.TeleportToPoint.Enable();
        Playthings.BareHands();
        //OutlineWorker.enabled = true;
        //OutlineWheel.enabled = true;
    }


    private void OnDisable() {
        //Teleport.TeleportToPoint.Disable();
        OutlineWorker.enabled = false;
        OutlineWheel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Phase2()
    {
         for (int i = 0; i < Phase1HotSpots.Count(); i++) {
                Debug.Log("Deactivating " + Phase1HotSpots[i]);
                Phase1HotSpots[i].SetActive(false);
                Debug.Log("Activating " + Phase2HotSpots[i]);
                Phase2HotSpots[i].SetActive(true);
            }
            Debug.Log("<color=red>Timeout</color>");
        
    }

    public void BillboardON()
    {
        Bill.SetActive(true);
    }
    public void BillboardOFF() 
    { 
        Bill.SetActive(false); 

    }

}
