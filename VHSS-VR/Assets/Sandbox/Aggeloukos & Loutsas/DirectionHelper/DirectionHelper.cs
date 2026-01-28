using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class DirectionHelper : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform Guide;
    public Transform[] Goals;
    public float CallAfter; //time untill the Direction helper kicks in
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; //Guide + the closest goal
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < CallAfter)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("DirectionHelper Active");
            lineRenderer.SetPosition(0, Guide.position);
            lineRenderer.SetPosition(1, ClosestGoal(Goals).position);
            Debug.Log("DirectionHelper guiding towards " + ClosestGoal(Goals));
        }
    }

    Transform ClosestGoal(Transform[] Goals)  //https://discussions.unity.com/t/clean-est-way-to-find-nearest-object-of-many-c/409917/2
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in Goals)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


}
