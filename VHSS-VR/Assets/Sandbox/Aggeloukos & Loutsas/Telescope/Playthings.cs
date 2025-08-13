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
    //public bool RightHolsterEmpty = false;
    public GameObject RightHolster;
    public GameObject Holster;
    //public InputAction TogglePistol;

    public GameObject[] grabbies;
    

    public InputAction ToggleRight;
    public InputAction ToggleLeft;



    // Start is called before the first frame update
  
    void OnEnable()
    {
        ToggleRight.Enable();
        ToggleLeft.Enable();
        Spyglass.SetActive(SpyglassVisible);
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
          

            for (int i = 0; i <= 23; i++) 
            {
                grabbies[i].SetActive(false);
            }
            
        }
    }
    public void OnToggle(InputAction.CallbackContext Context ) {
        Debug.Log("OnToggle");
    }

    public void HandlePlaythings(bool Hand, Holster holster) {
        
        if (holster.PistolInHolster()) {
            if (Hand && !PistolLeft.activeSelf) {
                RightPistolActive(true);
                holster.Pistol.SetActive(false);
                Debug.Log("Right Hand Took Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf) {
                LeftPistolActive(true);
                holster.Pistol.SetActive(false);
                Debug.Log("Left hand took weapon");
            }
           
        }
        else if (holster.HolsterIsEmpty()) {
            if (Hand && !PistolLeft.activeSelf && PistolRight.activeSelf) {
                RightPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Right Hand Holstered Weapon");
            }
            else if (!Hand && !PistolRight.activeSelf && PistolLeft.activeSelf) {
                LeftPistolActive(false);
                holster.Pistol.SetActive(true);
                Debug.Log("Left hand holstered weapon");
            }
            
        }
    }
}
