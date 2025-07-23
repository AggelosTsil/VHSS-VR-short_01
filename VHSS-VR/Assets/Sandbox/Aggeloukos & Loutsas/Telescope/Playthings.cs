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

    public GameObject Pistol;
    public AudioSource PewSound;
    public bool PistolVisible;
    //public InputAction TogglePistol;

    

    public InputAction Toggle;
    

        
    // Start is called before the first frame update
    void OnEnable()
    {
        Toggle.Enable();
        Spyglass.SetActive(SpyglassVisible);
        Pistol.SetActive(PistolVisible);
        GetSound.time = 0.15f;
    }

    private void OnDisable() {
        Toggle.Disable();
    }

    public void PistolActive(bool Active) {
        Pistol.SetActive(Active);
        if (Active) {
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
        Pistol.SetActive(false);
        Spyglass.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
       
    }
    public void OnToggle(InputAction.CallbackContext Context ) {
        Debug.Log("OnToggle");
    }
}
