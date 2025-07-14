using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public InputAction TeleportToPoint; //gets enabled in Explore
    public GameObject player;
    public Scenario Scenario;

    private Transform NewPosition;

    private LayerMask Anchors;

    GameObject AnchorObject;

    //<<Aux HotSpots>>
    public GameObject HS1;
    public GameObject HS2;
    public GameObject HS3;
    public GameObject HS4;
    public GameObject HS5;
    public GameObject HS6;

    //<<Aux HotSpot Bases>> we have the planning ahead skills of a goldfish
    public GameObject HS1Base;
    public GameObject HS2Base;
    public GameObject HS3Base;
    public GameObject HS4Base;
    public GameObject HS5Base;
    public GameObject HS6Base;

    //<<Hotspot Highlights>>
    public Outline OutlineWorker;
    public Outline OutlineWheel;
    public Material AuxActive;
    public Material AuxInactive;


    // Start is called before the first frame update
    void Start()
    {
        Anchors = LayerMask.GetMask("Anchors");
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (TeleportToPoint.IsPressed()) {
            RaycastHit hit;
            Debug.Log("Raycast sending");
            //Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 8f, Anchors)) 
            {

                if (hit.transform.gameObject.CompareTag("Activity Area")) {
                    //player.transform.position = hit.transform.gameObject.transform.position;
                    string goal = hit.transform.gameObject.name; //goal is used to define which of the two scenes is enabled
                    Debug.Log("GameObject name is " + goal);
                    if (goal == "Wheel"){
                        OutlineWheel.enabled = true;
                    } else if (goal == "Worker"){
                        OutlineWorker.enabled = true;
                    }
                    if (TeleportToPoint.IsPressed())
                    {
                        Scenario.EnterScene(goal, Scenario.Dialogue);
                        Debug.Log("Found Activity Area " + hit.transform.gameObject);
                    }
                }
                else if (hit.transform.gameObject.CompareTag("Auxiliary Hotspot")) 
                {
                    //Code that reveals highlights 
                    
                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                        switch (hit.transform.gameObject.name) {
                            case "Aux1":
                                //reveal highlights
                                HS6Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS6.transform.position;
                                }
                                break;
                            case "Aux2":
                                HS4Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS4.transform.position;
                                }
                                break;
                            case "Aux3":
                                HS3Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS3.transform.position;
                                }
                                break;
                            case "Aux4":
                                HS5Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS5.transform.position;
                                }
                                break;
                            case "Aux5":
                                HS1Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS1.transform.position;
                                }
                                break;
                            case "Aux6":
                                HS2Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS2.transform.position;
                                }
                                break;
                            Debug.Log("<color=red>Entered Auxiliary Hotspot</color>");
                        }
                    
                    //player.transform.position = hit.transform.gameObject.transform.position;
                    Debug.Log("Rayscast found solid target " + hit.transform.gameObject);
                }else 
                    {//highlights closing
                    OutlineWheel.enabled = false;
                    OutlineWorker.enabled = false;
                    HS1Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    HS2Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    HS3Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    HS4Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    HS5Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    HS6Base.GetComponent<MeshRenderer>().material = AuxInactive;
                    }

                    //player.transform.position = hit.transform.gameObject.transform.position;

                }else 
                {
                OutlineWheel.enabled = false;
                OutlineWorker.enabled = false;
                HS1Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS2Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS3Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS4Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS5Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS6Base.GetComponent<MeshRenderer>().material = AuxInactive;
                }
        //}
    }
}
