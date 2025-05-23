using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gamisouLoutsa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.A].wasPressedThisFrame){
            transform.position = new(0, 100, 0);
        }
    }
}
