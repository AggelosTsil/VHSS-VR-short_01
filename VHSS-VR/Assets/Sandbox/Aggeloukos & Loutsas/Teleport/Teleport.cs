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
    public GameObject currentHS;
    public GameObject AimingAt;
    public GameObject HSSecret;
    public GameObject HSSecretBase;
    public float timer = 0;
    public float cooldowntimer = 0;
    
    
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
            if (hit.transform.gameObject.CompareTag("Activity Area"))
            {
                string goal = hit.transform.gameObject.name; //goal is used to define which of the two scenes is enabled
                Debug.Log("GameObject name is " + goal);
                if (goal == "Wheel")
                {
                    OutlineWheel.enabled = true;
                }
                else if (goal == "Worker")
                {
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
                for (int i = 0; i <= HS.Count(); i++) //checks every Hotspot
                {
                    if (hit.transform.gameObject.name == $"Aux{i}") //checks if the player is pointing for Aux1, Aux2, etc 
                    {
                        AimingAt = HS[i];
                        HS[i].GetNamedChild("Teleport_Target").GetNamedChild("TT_Inactive").GetNamedChild("Base").GetComponent<MeshRenderer>().material = AuxActive;
                        if (HS[i].GetNamedChild("stairs") != null /*&&(!currentHS == HS[2] || !currentHS == HS[11] || !currentHS == HS[15] || !currentHS == HS[9])*/) //if the HS has a stairs child (and is not on the top of the stairs) then...
                        {
                            Debug.Log("STAIRSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                            HS[i].transform.GetChild(0).gameObject.SetActive(true); //turn on the Arrow 
                            HS[i].transform.GetChild(5).gameObject.SetActive(true); //turn on the Arrow  //and turn on the Outline 
                        }
                        cooldowntimer += Time.deltaTime;
                        if (TeleportToPoint.IsPressed())
                        {
                           
                                currentHS = HS[i]; //sets current hotspot
                                player.transform.position = HS[i].GetNamedChild("Teleport_Target").transform.position;
                                Scenario.EnterScene("Explore", Scenario.Dialogue);
                                cooldowntimer = 0;
                            
                        }
                    }
                }
                Debug.Log("Rayscast found solid target " + hit.transform.gameObject);
            }
            //secret room
            if (currentHS == HS[10]) {
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
        }
        else //if he stops pointing or pointing at nothing
          {
            OutlineWheel.enabled = false;
            OutlineWorker.enabled = false;
            
                AimingAt.GetNamedChild("Teleport_Target").GetNamedChild("TT_Inactive").GetNamedChild("Base").GetComponent<MeshRenderer>().material = AuxInactive; //turns off their "glow"
                if (AimingAt.GetNamedChild("stairs") != null) 
                {
                    AimingAt.transform.GetChild(0).gameObject.SetActive(false); //turn off the Arrow 
                    AimingAt.transform.GetChild(5).gameObject.SetActive(false); //and turn off the Outline 
                }
            
        }
        if (TeleportToPoint.IsPressed()) {
            Debug.Log("GUNSOUND");
            GunSound.Play();
        }
    }
}
    
            
                
            

    

