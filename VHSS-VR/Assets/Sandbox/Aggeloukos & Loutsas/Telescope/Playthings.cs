using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System;

public class Playthings : MonoBehaviour
{
    public GameObject SpyglassRight;
    public GameObject SpyglassLeft;
    public AudioSource GetSound; //this is in the spyglass parent game object
    public bool SpyglassVisible;
    //public InputAction ToggleSpyglass;

    public GameObject PistolRight;
    public GameObject PistolLeft;
    public AudioSource PewSoundRight;
    public AudioSource PewSoundLeft;
    public bool PistolVisible;
    //public bool RightHolsterEmpty = false;
    public GameObject RightHolster;
    public GameObject LeftHolster;
    //public InputAction TogglePistol;

    public GameObject[] grabbies;

    public GameObject prompt;
    public InputAction ToggleRight;
    public InputAction ToggleLeft;



    // Start is called before the first frame update
  
    void OnEnable()
    {
        ToggleRight.Enable();
        ToggleLeft.Enable();
        SpyglassRight.SetActive(SpyglassVisible);
        SpyglassLeft.SetActive(SpyglassVisible);
        PistolRight.SetActive(PistolVisible);
        PistolLeft.SetActive(PistolVisible);
        GetSound.time = 0.15f;
    }

    private void OnDisable() {
        ToggleRight.Disable();
        ToggleLeft.Disable();
    }

    public void RightPistolActive(bool Active) {
        PistolRight.SetActive(Active);
        if (Active) {
            GetSound.Play(); 
        }
    }

    public void LeftPistolActive(bool Active)
    {
        PistolLeft.SetActive(Active);
        if (Active)
        {
            GetSound.Play();
        }
    }

    public void RightSpyglassActive(bool Active) {
        SpyglassRight.SetActive(Active);
        if (Active) {
            GetSound.Play();
        }
    }

    public void LeftSpyglassActive(bool Active)
    {
        SpyglassLeft.SetActive(Active);
        if (Active)
        {
            //GetSound.Play();
        }
    }



    public void BareHands() {
        PistolRight.SetActive(false);
        PistolLeft.SetActive(false);
        SpyglassRight.SetActive(false);
        SpyglassLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!PistolRight.activeSelf && !PistolLeft.activeSelf && !SpyglassRight.activeSelf && !SpyglassLeft.activeSelf) 
        {
            for (int i = 0; i <= 23; i++) 
            {
                grabbies[i].SetActive(true);
            }
           
        }
        else {
          

            for (int i = 0; i <= 23; i++) 
            {
                grabbies[i].SetActive(false);
            }
            
        }

        //Empty Holster if's
        if (RightHolster.GetComponent<Holster>().HolsterIsEmpty())
        {
            RightHolster.GetComponent<Holster>().Empty.SetActive(true);
        }
        else
        {
            RightHolster.GetComponent<Holster>().Empty.SetActive(false);
        }

        if (LeftHolster.GetComponent<Holster>().HolsterIsEmpty())
        {
            LeftHolster.GetComponent<Holster>().Empty.SetActive(true);
        }
        else
        {
            LeftHolster.GetComponent<Holster>().Empty.SetActive(false);
        }

    }
    public void OnToggle(InputAction.CallbackContext Context ) {
        Debug.Log("OnToggle");
    }

    public void HandlePlaythings(bool Hand, Holster holster) {

        //----------GUN IS IN HOLSTER--------------
        if (holster.PistolInHolster())
        {
            if (Hand && !PistolLeft.activeSelf && SpyglassRight.activeSelf) //Right hand is holding the spyglass
            {
                RightPistolActive(true);
                holster.Pistol.SetActive(false);
                RightSpyglassActive(false);
                holster.Spyglass.SetActive(true);
                Debug.Log("Right Hand Took Weapon & Spyglass holstered");

            }
            else if (!Hand && !PistolRight.activeSelf && SpyglassLeft.activeSelf) //Left hand is holding the spyglass
            {
                LeftPistolActive(true);
                holster.Pistol.SetActive(false);
                LeftSpyglassActive(false);
                holster.Spyglass.SetActive(true);
                Debug.Log("Left hand took weapon & Spyglass holstered");
            }
            else if (Hand && !PistolLeft.activeSelf) //Right Hand Empty 
            {
                RightPistolActive(true);
                holster.Pistol.SetActive(false);
                Debug.Log("Right Hand Took Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf) //Left Hand empty
            {
                LeftPistolActive(true);
                holster.Pistol.SetActive(false);
                Debug.Log("Left hand took weapon");
            }
           
        }
        //----------SPY IS IN HOLSTER--------------
        else if (holster.SpyglassInHolster())
        {
            if (Hand && !SpyglassLeft.activeSelf && PistolRight.activeSelf) //Right hand is holding the gun
            {
                RightSpyglassActive(true);
                holster.Spyglass.SetActive(false);
                RightPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Right hand took Spyglass && Pistol Holstered");
            }
            else if (!Hand && !SpyglassRight.activeSelf && PistolLeft.activeSelf) //Left hand is holding the gun
            {
                LeftSpyglassActive(true);
                holster.Spyglass.SetActive(false);
                LeftPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Left hand took Spyglass & Pistol Holstered");
            }
            else if (Hand && !SpyglassLeft.activeSelf) //Right hand Empty
            {
                RightSpyglassActive(true);
                holster.Spyglass.SetActive(false);
                Debug.Log("Right hand took Spyglass");
            }
            else if (!Hand && !SpyglassRight.activeSelf) //Left hand Empty
            {
                LeftSpyglassActive(true);
                holster.Spyglass.SetActive(false);
                Debug.Log("Left hand took Spyglass");
            }
            
        }
        //----------NOTHING IS IN HOLSTER--------------
        else if (holster.HolsterIsEmpty())
        {
            if (Hand && !PistolLeft.activeSelf && PistolRight.activeSelf)
            {
                RightPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Right Hand Holstered Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf && PistolLeft.activeSelf)
            {
                LeftPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Left hand holstered weapon");
            }
            else if (Hand && !SpyglassLeft.activeSelf && SpyglassRight.activeSelf)
            {
                RightSpyglassActive(false);
                holster.Spyglass.SetActive(true);
                Debug.Log("Right Hand Holstered Spyglass");
            }
            else if (!Hand && !SpyglassRight.activeSelf && SpyglassLeft.activeSelf)
            {
                LeftSpyglassActive(false);
                holster.Spyglass.SetActive(true);
                Debug.Log("Left hand holstered Spyglass");
            }

        }  
    }

    public void Holstered(string fullorno)
    {
        if (fullorno == "full")
        {
            BareHands();
        }
   
        RightHolster.GetComponent<Holster>().Pistol.SetActive(true);
        

    }

    

    public void SpottingSetUp()
    {
        BareHands();
        RightHolster.GetComponent<Holster>().Pistol.SetActive(false);
        LeftHolster.GetComponent<Holster>().Pistol.SetActive(false);
        RightHolster.GetComponent<Holster>().Spyglass.SetActive(false);
        LeftHolster.GetComponent<Holster>().Spyglass.SetActive(false);

        RightHolster.GetComponent<Holster>().Pistol.SetActive(true);
        if (prompt == null) 
        {
            Debug.Log("qqqqqqqqqqqqqqq");
            LeftHolster.GetComponent<Holster>().Spyglass.SetActive(true);
        }

        

    }
}
