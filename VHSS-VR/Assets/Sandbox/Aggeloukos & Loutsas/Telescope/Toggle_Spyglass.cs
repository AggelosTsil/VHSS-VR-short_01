using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Toggle_Spyglass : MonoBehaviour
{
    public GameObject Spyglass;
    public AudioSource GetSound; //this is in the spyglass parent game object
    public bool Visible;
    public InputAction ToggleSpyglass;
    // Start is called before the first frame update
    void OnEnable()
    {
        ToggleSpyglass.Enable();
        Spyglass.SetActive(Visible);
        GetSound.time = 0.15f;
    }

    private void OnDisable() {
        ToggleSpyglass.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (ToggleSpyglass.triggered) {
            Visible = !Visible;
            Spyglass.SetActive(Visible);
            if (Visible) {
                
                GetSound.Play(); //plays the "Get" sound
            }
        }
    }
}
