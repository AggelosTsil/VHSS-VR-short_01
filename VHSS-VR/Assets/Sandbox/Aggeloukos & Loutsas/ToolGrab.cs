using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolGrab : MonoBehaviour
{
    public Playthings Playthings;
    public bool Hand;
    public Outline Outline;
    public GameObject Holster;
    private bool RightHolsterOut;
   
    
    // Start is called before the first frame update
    void Start()
    {
        switch (this.name) {
            case "RightHand":
                Hand = true;
                Debug.Log("<color=yellow>Right Hand ready to climb</color>");
                break;
            case "LeftHand":
                Hand = false;
                Debug.Log("<color=blue>Left Hand ready to climb</color>");
                break;
        }
        RightHolsterOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Playthings.PistolRight.activeSelf && !Playthings.PistolLeft.activeSelf) // If gun is not held, holster is available
        {
            Holster.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider other) {
        Debug.Log("Collision");
        if (other.CompareTag("Gun")) 
        {
            Outline.enabled = true;

            if (Playthings.Toggle.triggered && !RightHolsterOut)
            {
                if (Hand)
                {
                    Playthings.RightPistolActive(true);
                    RightHolsterOut = true;
                    Debug.Log("Right Hand Took Weapon");
                }
                else
                {
                    Playthings.LeftPistolActive(true);
                    RightHolsterOut = true;
                }
                Holster.SetActive(false);
            }
            else if (Playthings.Toggle.triggered && RightHolsterOut) 
            {
                if (Hand)
                {
                    Playthings.RightPistolActive(false);
                    RightHolsterOut = false;
                    Debug.Log("Right Hand Holstered Weapon");
                }
                else 
                { 
                    Playthings.LeftPistolActive(false);
                    RightHolsterOut = false;
                }
                Holster.SetActive(true);
            }
        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Gun")) {
            Outline.enabled = false;
        }
    }
}
