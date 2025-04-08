using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bousoulas : MonoBehaviour {

    [Header("Vertical straightening")]

    [SerializeField]
    private bool straighteningEnabled;

    [SerializeField]
    [Range(0, 1)]
    private float attractionCoefficient;

    protected void Start() {

    }

    protected void LateUpdate() {

        // Debug.Log(transform.rotation);

        if (straighteningEnabled) {

            Quaternion r = transform.rotation;
            // Vector3 t = r.eulerAngles;

            r.x = r.z = 0;
            r.Normalize();
            // t.x = t.z = 0;

            // transform.eulerAngles = t;
            // transform.rotation = r;

            Quaternion q = Quaternion.Slerp(transform.rotation, r, attractionCoefficient);

            if (q.w > 0.01f) { }

            transform.rotation = q;
        }
    }
}
