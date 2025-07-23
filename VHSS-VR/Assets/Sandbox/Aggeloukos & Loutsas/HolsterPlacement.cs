using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterPlacement : MonoBehaviour
{
    public GameObject player;
    public float x;
    public float z;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.rotation.x;
        z = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NewPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Quaternion NewRotation = new Quaternion(x, player.transform.rotation.y, z, player.transform.rotation.w);
        //transform.Rotate(NewRotation, Space.Self);
        transform.SetPositionAndRotation(NewPosition, NewRotation);

        Debug.Log("Position is " + NewPosition + " and rotation is " + NewRotation);

    }
}
