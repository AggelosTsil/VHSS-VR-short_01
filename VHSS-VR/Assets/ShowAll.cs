using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAll : MonoBehaviour
{
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log("Child: " + child.gameObject.name); //Debug to see all child objects
            child.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
