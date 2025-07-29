using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolGrab : MonoBehaviour
{
    public Playthings Playthings;
    public bool Hand;


 
   
    
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
            other.transform.gameObject.GetComponent<Holster>().GunOutlineON();
            Holster holster = other.transform.gameObject.GetComponent<Holster>();
            if ((Hand && Playthings.ToggleRight.triggered) || (!Hand && Playthings.ToggleLeft.triggered)) {
                Playthings.HandlePlaythings(Hand, holster);
            }
        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Gun")) {
            other.transform.gameObject.GetComponent<Holster>().GunOutlineOFF();
        }
    }
}
