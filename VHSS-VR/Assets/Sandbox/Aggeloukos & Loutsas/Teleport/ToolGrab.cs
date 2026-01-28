using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolGrab : MonoBehaviour
{
    public Playthings Playthings;
    public bool Hand;
    public Scenario scenario;

    // Start is called before the first frame update
    void Start()
    {
        switch (this.name) {
            case "RightHand":
                Hand = true;
                break;
            case "LeftHand":
                Hand = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerStay(Collider other) {
        Debug.Log("Collision");
        if (other.CompareTag("Gun")) 
        {
            Holster holster = other.transform.gameObject.GetComponent<Holster>();
            if (holster.PistolInHolster())
            {
                holster.GunOutlineON();
                if ((Hand && Playthings.ToggleRight.triggered) || (!Hand && Playthings.ToggleLeft.triggered))
                {
                    Playthings.HandlePlaythings(Hand, holster);
                    scenario.GunVoiceclip();
                }
            } else if (holster.SpyglassInHolster())
            {
                holster.SpyglassOutlineON();
                if ((Hand && Playthings.ToggleRight.triggered) || (!Hand && Playthings.ToggleLeft.triggered))
                {
                    Playthings.HandlePlaythings(Hand, holster);
                    //scenario.GunVoiceclip();
                }
            }
            else if (holster.HolsterIsEmpty()) 
            {
                if ((Hand && Playthings.ToggleRight.triggered) || (!Hand && Playthings.ToggleLeft.triggered))
                {
                    Playthings.HandlePlaythings(Hand, holster);
                    //scenario.GunVoiceclip();
                }
            }
        }

        if (other.CompareTag("Prompt"))
        {
            other.transform.gameObject.GetComponent<Prompt>().PromptOutlineON();
            if (Hand && Playthings.ToggleRight.triggered)
            {
                Destroy(other.transform.gameObject.GetComponent<Prompt>().prompt.transform.parent.gameObject);
                
                if (Playthings.PistolRight.activeSelf)
                {
                    Playthings.Holstered("full");
                }
                Playthings.SpyglassRight.SetActive(true);
                scenario.PromptVoiceclip();
            }
            else if (!Hand && Playthings.ToggleLeft.triggered)
            {
                other.transform.gameObject.GetComponent<Prompt>().prompt.transform.parent.gameObject.SetActive(false);
                if (Playthings.PistolLeft.activeSelf)
                {
                    Playthings.Holstered("full");
                }
                Playthings.SpyglassLeft.SetActive(true);
                scenario.PromptVoiceclip();
            }


        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Gun")) 
        {
            other.transform.gameObject.GetComponent<Holster>().GunOutlineOFF();
            other.transform.gameObject.GetComponent<Holster>().SpyglassOutlineOFF();
        }
        if (other.CompareTag("Prompt"))
        {
            other.transform.gameObject.GetComponent<Prompt>().PromptOutlineOFF();
        }
    }
}
