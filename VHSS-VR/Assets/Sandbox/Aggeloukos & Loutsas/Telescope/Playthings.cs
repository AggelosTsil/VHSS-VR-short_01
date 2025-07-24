using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Playthings : MonoBehaviour
{
    public GameObject Spyglass;
    public AudioSource GetSound; //this is in the spyglass parent game object
    public bool SpyglassVisible;
    //public InputAction ToggleSpyglass;

    public GameObject PistolRight;
    public GameObject PistolLeft;
    public AudioSource PewSoundRight;
    public AudioSource PewSoundLeft;
    public bool PistolVisible;
    public bool RightHolsterOut = false;
    public GameObject Holster;
    //public InputAction TogglePistol;

    public GameObject[] grabbies;
    

    public InputAction Toggle;



    // Start is called before the first frame update
  
    void OnEnable()
    {
        Toggle.Enable();
        Spyglass.SetActive(SpyglassVisible);
        PistolRight.SetActive(PistolVisible);
        PistolLeft.SetActive(PistolVisible);
        GetSound.time = 0.15f;
    }

    private void OnDisable() {
        Toggle.Disable();
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

    public void SpyglassActive(bool Active) {
        Spyglass.SetActive(Active);
        if (Active) {
            GetSound.Play();
        }
    }

    public void BareHands() {
        PistolRight.SetActive(false);
        PistolLeft.SetActive(false);
        Spyglass.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!PistolRight.active && !PistolLeft.active && !Spyglass.active) 
        {
            for (int i = 0; i <= 23; i++) 
            {
                grabbies[i].SetActive(true);
            }
           
        }
        else {
            Debug.Log("IT HAS OPENED");

            for (int i = 0; i <= 23; i++) 
            {
                grabbies[i].SetActive(false);
            }
            
        }
    }
    public void OnToggle(InputAction.CallbackContext Context ) {
        Debug.Log("OnToggle");
    }

    public void HandlePlaythings(bool Hand) {
   
        if (Toggle.triggered && !RightHolsterOut) {
            if (Hand && !PistolLeft.activeSelf) {
                RightPistolActive(true);
                Holster.SetActive(false);
                RightHolsterOut = true;
                Debug.Log("Right Hand Took Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf) {
                LeftPistolActive(true);
                Holster.SetActive(false);
                RightHolsterOut = true;
            }
           
        }
        else if (Toggle.triggered && RightHolsterOut) {
            if (Hand && !PistolLeft.activeSelf) {
                RightPistolActive(false);
                Holster.SetActive(true);
                RightHolsterOut = false;
                Debug.Log("Right Hand Holstered Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf) {
                LeftPistolActive(false);
                Holster.SetActive(true);
                RightHolsterOut = false;
            }
            
        }
    }
}
