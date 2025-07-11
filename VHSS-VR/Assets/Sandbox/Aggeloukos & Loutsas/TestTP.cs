using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestTP : MonoBehaviour
{
    public Transform Player;
    public GameObject[] Flags;
    public int i = 0;
    public InputAction Teleport;
    public InputAction EquipTelescope;
    public GameObject Telescope;
    // Start is called before the first frame update
    void Start()
    {
        Teleport.Enable();
        EquipTelescope.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Keyboard.current[Key.A].wasPressedThisFrame) || (Teleport.triggered))  
        {
            Player.transform.position = Flags[i].transform.position;
            i++;
            if (i <= 3)
            {
                Player.transform.rotation = new(0, 0, 0, 0);
            }
            else
            {
                Player.transform.rotation = new(0, 90, 0, 0);
            }
            if (i >= 6)
            {
                i = 0;
            }

        }

        if (EquipTelescope.triggered)
        {
            Telescope.active = !Telescope.active;
        }
    }

}
