using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject Cannon;
    private Outline Outline;
    private AudioSource BoomSound;
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
      
        Animator = Cannon.GetComponent<Animator>();
        Outline = Cannon.GetComponent<Outline>();
        BoomSound = Cannon.GetComponent<AudioSource>();
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
            BoomSound.Play();
            Animator.SetTrigger("Event");
        
    }
   
}
