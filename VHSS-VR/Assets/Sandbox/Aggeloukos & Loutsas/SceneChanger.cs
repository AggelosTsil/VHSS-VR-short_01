using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public InputAction holdbutton;
    public string targetScene;
    public float holdDuration = 5f;

    private float holdTimer = 0f;

    public void OnEnable()
    {
        holdbutton.Enable();
    }

    public void OnDisable()
    {
        holdbutton.Disable();
    }

    void Update()
    {
        
        if (holdbutton.IsPressed())
        {
            holdTimer += Time.deltaTime;
            Debug.Log(holdTimer);
            if (holdTimer >= holdDuration)
            {
                SceneManager.LoadScene(targetScene);
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }
}
