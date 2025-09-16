using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyglassRaycast : MonoBehaviour
{
    private LayerMask Anchors;
    public Scenario scenario;
    // Start is called before the first frame update
    void Start()
    {
        Anchors = LayerMask.GetMask("Anchors");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.Log("Spyglass Raycast sending");
        //Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
        if (Physics.Raycast(transform.position, transform.right, out hit, Mathf.Infinity, Anchors))
        {
            if (hit.transform.gameObject.CompareTag("Fregata"))
            {
                Debug.Log("HIT");
                scenario.FregataVC();
            }
        }
    }
}
