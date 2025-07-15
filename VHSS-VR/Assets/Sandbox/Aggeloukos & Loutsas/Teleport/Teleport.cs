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
    public GameObject HS7;
    public GameObject HS8;
    public GameObject HS9;
    public GameObject HS10;
    public GameObject HS11;
    public GameObject HS12;
    public GameObject HS13;
    public GameObject HS14;

    public GameObject HSSecret;

    //<<Aux HotSpot Bases>> we have the planning ahead skills of a goldfish
    public GameObject HS1Base;
    public GameObject HS2Base;
    public GameObject HS3Base;
    public GameObject HS4Base;
    public GameObject HS5Base;
    public GameObject HS6Base;
    public GameObject HS7Base;
    public GameObject HS8Base;
    public GameObject HS9Base;
    public GameObject HS10Base;
    public GameObject HS11Base;
    public GameObject HS12Base;
    public GameObject HS13Base;
    public GameObject HS14Base;

    public GameObject HSSecretBase;


    //Misc
    public GameObject Arrow1;
    public GameObject Arrow2;
    public Outline OutlineStairs1;
    public Outline OutlineStairs2;
    public float timer = 0;
    public bool SecretFound;
    public bool Aux10;

    //<<Hotspot Highlights>>
    public Outline OutlineWorker;
    public Outline OutlineWheel;
    public Material AuxActive;
    public Material AuxInactive;

    public AudioSource GunSound;

    

    // Start is called before the first frame update
    void Start()
    {
        Anchors = LayerMask.GetMask("Anchors");
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;
            Debug.Log("Raycast sending");
            //Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 8f, Anchors)) 
            {

                if (hit.transform.gameObject.CompareTag("Activity Area")) {
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
                        switch (hit.transform.gameObject.name) {
                            case "Aux1":
                                //reveal highlights
                                HS6Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS6.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux2":
                                HS4Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS4.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux3":
                                HS3Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS3.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux4":
                                HS5Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS5.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux5":
                                HS1Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS1.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux6":
                                HS2Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS2.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux7":
                                HS7Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS7.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                }
                                break;
                            case "Aux8":
                                HS8Base.GetComponent<MeshRenderer>().material = AuxActive;
                                Arrow1.SetActive(true);
                                OutlineStairs1.enabled = true;
                                if (TeleportToPoint.IsPressed())
                                {
                                player.transform.position = HS8.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                Arrow1.SetActive(false);
                                }
                                break;
                            case "Aux9":
                                HS9Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed()) {
                                player.transform.position = HS9.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                Aux10 = false;
                                }
                                break;
                            case "Aux10":
                                HS10Base.GetComponent<MeshRenderer>().material = AuxActive;
                                if (TeleportToPoint.IsPressed()) {
                                player.transform.position = HS10.transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                Aux10 = true;
                                }
                        
                                break;
                            case "Aux11":
                                HS11Base.GetComponent<MeshRenderer>().material = AuxActive;
                                Arrow2.SetActive(true);
                                OutlineStairs2.enabled = true;
                                if (TeleportToPoint.IsPressed()) {
                                    player.transform.position = HS11.transform.position;
                                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                                    Arrow2.SetActive(false);
                                    }
                                break;
                            case "Aux12":
                                HS12Base.GetComponent<MeshRenderer>().material = AuxActive;
                               
                                if (TeleportToPoint.IsPressed()) {
                                    player.transform.position = HS12.transform.position;
                                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                                    
                                }
                                break;
                            case "Aux13":
                                HS13Base.GetComponent<MeshRenderer>().material = AuxActive;
                                
                                if (TeleportToPoint.IsPressed()) {
                                    player.transform.position = HS13.transform.position;
                                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                                   
                                }
                                break;
                            case "Aux14":
                                HS14Base.GetComponent<MeshRenderer>().material = AuxActive;
                               
                                if (TeleportToPoint.IsPressed()) {
                                    player.transform.position = HS14.transform.position;
                                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                                    
                                }
                                break;

                           
                        }
                    
                    Debug.Log("Rayscast found solid target " + hit.transform.gameObject);
                }else 
                    {//highlights closing
                    
            }


                }else 
                {
                OutlineWheel.enabled = false;
                OutlineWorker.enabled = false;
                OutlineStairs1.enabled = false;
                OutlineStairs2.enabled = false;

                HS1Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS2Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS3Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS4Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS5Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS6Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS7Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS8Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS9Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS10Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS11Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS12Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS13Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HS14Base.GetComponent<MeshRenderer>().material = AuxInactive;
                HSSecretBase.GetComponent<MeshRenderer>().material = AuxInactive;
                Arrow1.SetActive(false);
                Arrow2.SetActive(false);
        }
            if (TeleportToPoint.IsPressed())
            {
                Debug.Log("GUNSOUND");
                GunSound.Play();
            }
            if (Aux10) {
            if (hit.transform.gameObject.name == "AuxSecret") {
                HSSecretBase.GetComponent<MeshRenderer>().material = AuxActive;

                if (TeleportToPoint.IsPressed()) {
                    player.transform.position = HSSecret.transform.position;
                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                    SecretFound = true;
                }
            }
        }
            if (SecretFound) 
            {
                timer += Time.deltaTime;
            if (timer >= 5f) {
                player.transform.position = HS10.transform.position;
                Scenario.EnterScene("Explore", Scenario.Dialogue);
                SecretFound = false;
                timer = 0;
            }
            }
    }
}
