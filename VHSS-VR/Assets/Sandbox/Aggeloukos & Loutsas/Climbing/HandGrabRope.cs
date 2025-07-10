using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabRope : MonoBehaviour
{
    bool Hand; //Which hand the object is True is right False is left
    // Start is called before the first frame update
    public NewClimbing NewClimbing;
    public Collider col;
    void Start()
    {
        //<<If you want to hire us please ignore this>>
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

    void OnTriggerStay(Collider other) {
        Debug.Log("<bold>Collision</bold>");
        NewClimbing.GrabMove(Hand);
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }


}
