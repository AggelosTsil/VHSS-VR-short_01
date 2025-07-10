using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public InputAction TeleportToPoint; //gets enabled in Explore
    public GameObject player;

    private Transform NewPosition;

    LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Anchors");
    }

    // Update is called once per frame
    void Update()
    {
        if (TeleportToPoint.IsPressed()) {
            RaycastHit hit;
            Debug.Log("Raycast sending");
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) 
            {
                if (hit.transform.gameObject.CompareTag("Activity Area")) {
                    player.transform.position = hit.transform.gameObject.transform.position;
                    Debug.Log("Found Activity Area");
                }

                Debug.Log("Rayscast found solid target");
               
            }
        }
    }
}
