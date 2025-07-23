using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolGrab : MonoBehaviour
{
    public Playthings Playthings;
    private bool Hand;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other) {
        Debug.Log("Collision");
        if (other.CompareTag("Gun") && Playthings.Toggle.triggered) 
        {
            Playthings.PistolActive(true);
        }

        }
}
