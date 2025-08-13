using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Teleport : MonoBehaviour
{
    public InputAction TeleportToPoint; //gets enabled in Explore
    public GameObject player;
    public Scenario Scenario;

    private Transform NewPosition;

    private LayerMask Anchors;

    GameObject AnchorObject;
    
    public GameObject[] HS;
    public string currentAux;
    public GameObject currentHS;
    public GameObject AimingAt;
    public GameObject HSSecret;
    public GameObject HSSecretBase;
    public float timer = 0;
    public float cooldowntimer = 0;
    
    //<<Hotspot Highlights>>
    public Outline OutlineWorker;
    public Outline OutlineWheel;
    //public Outline OutlineEscapeRopes;
    public Outline OutlineClimbing;
    public Material AuxActive;
    public Material AuxInactive;
    
    //Gun Stuff
    public AudioSource GunSound;
    public Animator Recoil;

    //Fun
    public GameObject CannonAux;
    public ParticleSystem Smoke;
    public GameObject[] Cannon;

    void Start()
    {
        Anchors = LayerMask.GetMask("Anchors");
    }
    void Update()
    {
        RaycastHit hit;
        Debug.Log("Raycast sending");
        //Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 8f, Anchors))
        {
            if (hit.transform.gameObject.CompareTag("Activity Area"))
            {
                string goal = hit.transform.gameObject.name; //goal is used to define which of the two scenes is enabled
                Debug.Log("GameObject name is " + goal);
                switch (goal) {
                    case "Wheel":
                        ActivityOutline(OutlineWheel, 1);
                        //OutlineWheel.enabled = true;
                        break;
                    case "Worker":
                        ActivityOutline(OutlineWorker, 2);
                        //OutlineWorker.enabled = true;
                        break;
                    case "Climbing":
                        ActivityOutline(OutlineClimbing, 3);
                        //OutlineClimbing.enabled = true;
                        break;
                }

                if (TeleportToPoint.triggered)
                {
                    if (goal == "Spotting") {
                        player.transform.position = hit.transform.gameObject.transform.position;
                    }
                    Scenario.EnterScene(goal, Scenario.Dialogue);
                    Debug.Log("Found Activity Area " + hit.transform.gameObject);
                    currentHS.SetActive(true);
                }
            }
            else if (hit.transform.gameObject.CompareTag("Auxiliary Hotspot"))
            {
                for (int i = 0; i <= HS.Count(); i++) //checks every Hotspot
                {
                    if (hit.transform.gameObject.name == $"Aux{i}") //checks if the player is pointing for Aux1, Aux2, etc 
                    {
                        CloseAllOutlines(0 , i, 0);
                        AimingAt = HS[i];
                        OpenHS();
                        if (TeleportToPoint.triggered)
                        {
                            currentHS.SetActive(true);
                            currentHS.transform.GetChild(3).gameObject.SetActive(true);
                            currentHS.transform.GetChild(4).gameObject.SetActive(false);
                            currentAux = hit.transform.gameObject.name; //sets current hotspot
                            currentHS = HS[i];
                            player.transform.position = HS[i].GetNamedChild("Teleport_Target").transform.position;
                            Scenario.EnterScene("Explore", Scenario.Dialogue);
                            currentHS.SetActive(false);
                        }
                    }
                }
                Debug.Log("Rayscast found solid target " + hit.transform.gameObject);
            }
            //cannon
            if (hit.transform.gameObject.CompareTag("Cannon"))
            {
                for (int i = 0; i <= Cannon.Count(); i++) //checks every 
                {
                    if (hit.transform.gameObject.name == $"CannonAux{i}") //checks if the player is pointing for Aux1, Aux2, etc 
                    {
                        CloseAllOutlines(0, 0, i);
                        CannonAux = Cannon[i];
                        CannonOutline(CannonAux);
                        if (TeleportToPoint.triggered)
                        { 
                            CannonAux.GetComponent<Canon>().CanonEvent();
                            CannonAux.SetActive(false);
                        }
                    }
                }
            }
            //secret room
            if (currentAux == "Aux10")
            {
                if (hit.transform.gameObject.name == "AuxSecret")
                {
                    if (TeleportToPoint.IsPressed())
                    {
                        player.transform.position = HSSecret.transform.position;
                        Scenario.EnterScene("Explore", Scenario.Dialogue);
                        timer += Time.deltaTime;
                        if (timer >= 5f)
                        {
                            player.transform.position = HS[10].transform.position;
                            Scenario.EnterScene("Explore", Scenario.Dialogue);
                            timer = 0;
                        }
                    }
                }
            }

        }
        else //if he stops pointing or pointing at nothing
        {
            CloseAllOutlines();
        }
        if (TeleportToPoint.triggered) {
            Debug.Log("GUNSOUND");
            GunSound.Play();
            Recoil.SetTrigger("Shoot");
            Smoke.Play();
        }
        
    }
    public void CloseAllOutlines(int ActivityNum = 0, int HotspotNum = 0, int CannonNum = 0) // 0 = null, 1 = Wheel, 2 = Worker, 3 = Climbing && 
    {
        CloseActivityOutline(ActivityNum);
        CloseCannonOutline(CannonNum);
        CloseHS(HotspotNum);
    }

    public void CloseHS(int HotspotNum)
    {
        for (int i = 0; i < HS.Count(); i++) //checks every Hotspot
        {
            if (HS[i] != HS[HotspotNum])
            {
                HS[i].transform.GetChild(3).gameObject.SetActive(true);
                HS[i].transform.GetChild(4).gameObject.SetActive(false);
                if (HS[i].GetNamedChild("stairs") != null)
                {
                    HS[i].transform.GetChild(0).gameObject.SetActive(false); //turn off the Arrow 
                    HS[i].transform.GetChild(7).gameObject.SetActive(false); //and turn off the Outline 
                }
            }
        }
    }
    public void OpenHS()
    {
        AimingAt.transform.GetChild(3).gameObject.SetActive(false);
        AimingAt.transform.GetChild(4).gameObject.SetActive(true);
        if (AimingAt.GetNamedChild("stairs") != null) //if the HS has a stairs child then...
        {
            AimingAt.transform.GetChild(0).gameObject.SetActive(true); //turn on the Arrow 
            AimingAt.transform.GetChild(7).gameObject.SetActive(true); //and turn on the Outline 
        }
    }
    public void CloseCannonOutline(int CannonNum)
    {
        for (int i = 0; i < Cannon.Count(); i++) //checks every Hotspot
        {
            if (CannonAux != null) {
                if (Cannon[i] != Cannon[CannonNum])
                {
                        Debug.Log("Cannon Outline GONE" + i);
                        Cannon[i].GetComponent<Canon>().CloseOutline();
                }
            }
        }
    }
    public void CannonOutline(GameObject Cannon)
    {
        if (Cannon != null)
        {
            Cannon.GetComponent<Canon>().ShowOutline();
        }
    }
    public void ActivityOutline(Outline currentOutline, int y = 0)
    {
        switch (y)
        {
            case 0:
                CloseAllOutlines(y, 0, 0);
                break;
            case 1:
                CloseAllOutlines(y, 0, 0);
                break;
            case 2:
                CloseAllOutlines(y, 0, 0);
                break;
            case 3:
                CloseAllOutlines(y, 0, 0);
                break;

        }
        if (currentOutline != null)
        {
            currentOutline.enabled = true;
        }
    }
    public void CloseActivityOutline(int ActivityNum)
    {
        switch (ActivityNum)
        {
            case 0:
                OutlineWheel.enabled = false;
                OutlineWorker.enabled = false;
                OutlineClimbing.enabled = false;
                break;
            case 1:
                OutlineWorker.enabled = false;
                OutlineClimbing.enabled = false;
                break;
            case 2:
                OutlineWheel.enabled = false;
                OutlineClimbing.enabled = false;
                break;
            case 3:
                OutlineWheel.enabled = false;
                OutlineWorker.enabled = false;
                break;
        }

    }
}
    
            
                
            

    

