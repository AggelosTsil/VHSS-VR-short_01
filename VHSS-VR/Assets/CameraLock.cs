using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Quaternion NewRotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.SetPositionAndRotation(NewPosition, NewRotation); 
    }
}
