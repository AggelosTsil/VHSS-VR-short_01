using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    //public InputAction TogglePistol;

    

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
       
    }
    public void OnToggle(InputAction.CallbackContext Context ) {
        Debug.Log("OnToggle");
    }
}
