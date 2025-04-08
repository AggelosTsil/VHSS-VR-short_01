using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpExit : MonoBehaviour
{ 
public GameObject Instructions1;
public GameObject Instructions2;
public GameObject HotSpot;


public GripButtonPress watcher;
public bool IsPressed = false; // public to show button state in the Unity Inspector window


void Start()
{
    watcher.gripButtonPress.AddListener(onGripButtonEvent);


}

public void onGripButtonEvent(bool pressed)
{
    IsPressed = pressed;

    if (pressed)
        Invoke("FadeOut", 2.0f);

}

private void FadeOut()
{
    Instructions1.SetActive(false);
    Instructions2.SetActive(true);
    HotSpot.SetActive(true);
   

}

}