using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    public GameObject Instructions;
    public AudioSource popUp;
    public void Initiate()
    {
        Invoke("FadeIn", 3.0f);
        popUp = GetComponent<AudioSource>();
    }


    private void FadeIn()
    {
        Instructions.SetActive(true);
        popUp.Play();
    }


}
