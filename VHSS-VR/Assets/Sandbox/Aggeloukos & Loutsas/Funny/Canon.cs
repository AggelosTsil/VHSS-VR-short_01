using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Canon : MonoBehaviour
{

    public Outline Outline;
    public AudioSource BoomSound;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        BoomSound = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ShowOutline()
    {
        Outline.enabled = true;
    }
    public void CloseOutline() 
    { 
        Outline.enabled = false;
    }

    public void CanonEvent()
    {
        
            Outline.enabled = false;
            this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            BoomSound.time = 0.15f;
            BoomSound.Play();
            Animator.SetTrigger("Event");
        
    }
}
