using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class DirectionHelper : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool active;
    public float LineWidth; //change it from here and not the line renderer
    public Transform Guide;
    public List<Transform> Goals;
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
        if (active)
        {
            if (timer < CallAfter)
            {
                timer += Time.deltaTime;
            }
            else if (Goals.Count > 0) //if there are goals in the list then it draws between the guidepoint and the closest goal
            {
                lineRenderer.startWidth =LineWidth;
                lineRenderer.endWidth = LineWidth;
                Debug.Log("DirectionHelper Active");
                lineRenderer.SetPosition(0, Guide.position);
                lineRenderer.SetPosition(1, ClosestGoal(Goals).position);
                Debug.Log("DirectionHelper guiding towards " + ClosestGoal(Goals));
            }
        }
        else 
            {
                lineRenderer.startWidth = 0;
                lineRenderer.endWidth = 0;
            }
    }

    public void CleanGoals()
    {
        Goals.Clear();
        Debug.Log("cleaned Goals");
    }

    public void AddGoal(Transform goal)
    {
        Goals.Add(goal); 
        Debug.Log("added Goal " + goal);
    }

    public void RemoveGoal(Transform target)
    {
        Goals.Remove(target);
        Debug.Log("removed Goal " + target + "goals are now " + Goals);
    }

    public void StartTiming(float cutTime = 0)
    {
        timer = cutTime;
        active = true;
        Debug.Log("helper started timing");
    }

    public void GoalMet()
    {
        active = false;
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
        Debug.Log("goal met");
    }

    public bool SpottingArrowOnly() {
        return ((Goals.Count == 1) && (Goals[0].name == "LeaveArrow"));
    }

    Transform ClosestGoal(List<Transform> Goals)  //https://discussions.unity.com/t/clean-est-way-to-find-nearest-object-of-many-c/409917/2
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
