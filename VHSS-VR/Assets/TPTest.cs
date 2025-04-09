using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPTest : MonoBehaviour
{
    public GameObject Player;
    public Transform Position;
    public Vector3 TPpoint;
    // Start is called before the first frame update
    void Start()
    {
        TPpoint = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision with Player");
            Player.transform.position = TPpoint;
        }
    }
}
