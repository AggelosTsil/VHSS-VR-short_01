using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPopUp : MonoBehaviour
{

    public GameObject otherGameObject;
    public AudioSource popUp;
    void Start()
    {
        Invoke("FadeIn", 5.0f);
    }


    public void FadeIn()
    {
        otherGameObject.SetActive(true);
        popUp.Play();
    }


}
