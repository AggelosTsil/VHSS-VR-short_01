using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnim : MonoBehaviour
{
    public Animator animator;
    public float increase = 0.1f;
    float oldRotation;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        oldRotation = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dr = transform.localEulerAngles.y - oldRotation;
        oldRotation = transform.localEulerAngles.y;
        if (Mathf.Abs(dr) > 0) {
            time += increase * dr;
            animator.SetFloat("time", time );
        }
    }
}
