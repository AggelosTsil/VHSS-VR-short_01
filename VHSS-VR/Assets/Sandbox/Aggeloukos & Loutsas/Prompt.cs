using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public GameObject prompt;
    private Outline promptOutline;
    // Start is called before the first frame update
    void Start()
    {
        promptOutline = prompt.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PromptOutlineON()
    {
        promptOutline.enabled = true;
    }

    public void PromptOutlineOFF()
    {
        promptOutline.enabled = false;
    }
}
