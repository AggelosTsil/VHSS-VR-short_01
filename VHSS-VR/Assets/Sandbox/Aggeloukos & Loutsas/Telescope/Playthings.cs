using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playthings : MonoBehaviour
{
    public GameObject Spyglass;
    public AudioSource GetSound; //this is in the spyglass parent game object
    public bool SpyglassVisible;
    public InputAction ToggleSpyglass;

    public GameObject Pistol;
    public AudioSource PewSound;
    public bool PistolVisible;
    public InputAction TogglePistol;

    // Start is called before the first frame update
    void OnEnable()
    {
        ToggleSpyglass.Enable();
        TogglePistol.Enable();
        Spyglass.SetActive(SpyglassVisible);
        Pistol.SetActive(PistolVisible);
        GetSound.time = 0.15f;
    }

    private void OnDisable() {
        ToggleSpyglass.Disable();
        TogglePistol.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (ToggleSpyglass.triggered) 
        {
            SpyglassVisible = !SpyglassVisible;
            Spyglass.SetActive(SpyglassVisible);
            if (SpyglassVisible) 
            {
                GetSound.Play(); //plays the "Get" sound
            }else 
            {
                PistolVisible = !PistolVisible;
                Pistol.SetActive(PistolVisible);
                GetSound.Play();
            }
        }
        else if (TogglePistol.triggered)
        {
            PistolVisible = !PistolVisible;
            Pistol.SetActive(PistolVisible);
                GetSound.Play();
        }
    }
}
