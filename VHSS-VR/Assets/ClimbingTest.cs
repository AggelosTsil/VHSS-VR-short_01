using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbingTest : MonoBehaviour
{
    public InputAction Grabbing;
    public InputAction Moving;
    public GameObject Player;
    public Collider Grabbable;
    public Vector3 MoveVector;

    // Start is called before the first frame update

    void Start()
    {
        
    }
    public void OnMoving(InputAction.CallbackContext context)
    {
        MoveVector = Moving.ReadValue<Vector3>();
    }
    // Update is called once per frame
    void Update()
    {
        Grabbing.Enable();
        if (Grabbing.triggered)
        {
            Moving.Enable();
            Debug.Log("Moving is enabled");
            Player.transform.position += MoveVector;
        }
        else
        {
            //Debug.Log("Moving is not abled");
        }
    }

    
    private void OnTriggerStay(Collider Grabbable)
    {
        Grabbing.Enable();
        if (Grabbing.triggered)
        {
            Moving.Enable();
            Debug.Log("Moving is enabled");
        }
        else
        {
            //Debug.Log("Moving is not abled");
        }
    }
}
