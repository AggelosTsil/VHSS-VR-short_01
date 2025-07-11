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

    public GameObject HS1;
    public GameObject HS2;
    public GameObject HS3;
    public GameObject HS4;
    public GameObject HS5;
    public GameObject HS6;

    // Start is called before the first frame update
    void Start()
    {
        Anchors = LayerMask.GetMask("Anchors");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TeleportToPoint.IsPressed()) {
            RaycastHit hit;
            Debug.Log("Raycast sending");
            Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, Anchors)) 
            {

                if (hit.transform.gameObject.CompareTag("Activity Area")) {
                    //player.transform.position = hit.transform.gameObject.transform.position;
                    string goal = hit.transform.gameObject.name; //goal is used to define which of the two scenes is enabled
                    Scenario.EnterScene(goal, Scenario.Dialogue);
                    Debug.Log("Found Activity Area " + hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Auxiliary Hotspot")) {
                    Debug.Log("<color=red>Entered Auxiliary Hotspot</color>");
                    Scenario.EnterScene("Explore", Scenario.Dialogue);
                    switch (hit.transform.gameObject.name) {
                        case "Aux1":
                            player.transform.position = HS6.transform.position;
                            break;
                        case "Aux2":
                            player.transform.position = HS4.transform.position;
                            break;
                        case "Aux3":
                            player.transform.position = HS3.transform.position;
                            break;

                    }
                    //player.transform.position = hit.transform.gameObject.transform.position;
                    Debug.Log("Rayscast found solid target " + hit.transform.gameObject);
                }

                    //player.transform.position = hit.transform.gameObject.transform.position;

                }
        }
    }
}
