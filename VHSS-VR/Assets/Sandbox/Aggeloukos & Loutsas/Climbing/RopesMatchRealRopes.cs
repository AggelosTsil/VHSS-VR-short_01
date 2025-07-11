using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopesMatchRealRopes : MonoBehaviour
{
    public GameObject RealRopes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(RealRopes.transform.position, RealRopes.transform.rotation);
    }
}
