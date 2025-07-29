using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
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
    public Outline OutlineEscapeRopes;
    public Outline OutlineClimbing;
    public Material AuxActive;
    public Material AuxInactive;
    
    //Gun Stuff
    public AudioSource GunSound;
    public Animator Recoil;

    //Fun
    public GameObject CannonAux;
    public ParticleSystem Smoke;

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
            if (hit.transform.gameObject.CompareTag("Activity Area"))
            {
                string goal = hit.transform.gameObject.name; //goal is used to define which of the two scenes is enabled
                Debug.Log("GameObject name is " + goal);
                switch (goal) {
                    case "Wheel":
                        OutlineWheel.enabled = true;
                        break;
                    case "Worker":
                        OutlineWorker.enabled = true;
                        break;
                    case "Climbing":
                        OutlineClimbing.enabled = true;
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
                        AimingAt = HS[i];
                        HS[i].transform.GetChild(3).gameObject.SetActive(false);
                        HS[i].transform.GetChild(4).gameObject.SetActive(true);
                        if (HS[i].GetNamedChild("stairs") != null) //if the HS has a stairs child (and is not on the top of the stairs) then...
                        {
                            //Debug.Log("STAIRSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                            HS[i].transform.GetChild(0).gameObject.SetActive(true); //turn on the Arrow 
                            HS[i].transform.GetChild(7).gameObject.SetActive(true); //turn on the Arrow  //and turn on the Outline 
                        }
                   
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
            //secret room
            if (currentAux == "Aux10") {
                if (hit.transform.gameObject.name == "AuxSecret") {
                    if (TeleportToPoint.IsPressed()) {
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

         
            //cannon
            if (hit.transform.gameObject.CompareTag("Cannon"))
            {
                CannonAux = hit.transform.gameObject;
                CannonAux.GetComponent<Canon>().ShowOutline();
                //Canon.GetComponent<Canon>().CloseOutline();
                if (TeleportToPoint.triggered) 
                {

                    CannonAux.GetComponent<Canon>().CanonEvent();
                    Destroy(CannonAux);


                }
            }else if (CannonAux != null) {
                CannonAux.GetComponent<Canon>().CloseOutline();
            }   
            
        }
        else //if he stops pointing or pointing at nothing
          {
            OutlineWheel.enabled = false;
            OutlineWorker.enabled = false;
            OutlineClimbing.enabled = false;
            OutlineEscapeRopes.enabled = false;
            if (CannonAux != null) {
                CannonAux.GetComponent<Canon>().CloseOutline();
            }

            AimingAt.transform.GetChild(3).gameObject.SetActive(true);
            AimingAt.transform.GetChild(4).gameObject.SetActive(false);
            if (AimingAt.GetNamedChild("stairs") != null) 
            {
                AimingAt.transform.GetChild(0).gameObject.SetActive(false); //turn off the Arrow 
                AimingAt.transform.GetChild(7).gameObject.SetActive(false); //and turn off the Outline 
            }
            
        }
        if (TeleportToPoint.triggered) {
            Debug.Log("GUNSOUND");
            GunSound.Play();
            Recoil.SetTrigger("Shoot");
            Smoke.Play();
         

        }
    }
}
    
            
                
            

    

