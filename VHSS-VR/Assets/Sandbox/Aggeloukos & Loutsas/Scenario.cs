using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public bool Timer; //Togles timer function  
    public float NextSceneTimer = 3;//The time it takes for a scene to auto-skip
    public float TimeUntillNextScene;//The time left for the current scene
    public GameObject Player;
    public GameObject[] Flags;//Teleport hotspots
    public int i = 0;//Good ol' reliable
    public AudioSource SeagullSpeaking; //The source from where you hear the seagul speak
    public AudioClip[] Seagull_Dialogues; //The different things the seagull says
    public bool Dialogue;//Shuts up the seagull
    //<<Activity Scripts>>
    public GameObject ExploreActivity;
    public GameObject WorkerActivity;
    public GameObject WheelActivity;
    public GameObject ClimbingActivity;
    public GameObject SpottingActivity;
    public GameObject EndindActivity;
    public Animator Blurs;

    //<<End of Activity Scripts>>
    
     public void EnterScene(int i, bool Dialogue) { //Initiallises next scene <<NOTE ADD i++>>
        Debug.Log("<color=yellow>Entered Scene </color>" + Flags[i]);
        TimeUntillNextScene = NextSceneTimer; //Initiallises/resets timer
        if (i <= 3) {
            Player.transform.rotation = new(0, 0, 0, 0); //Rotational changes are purely to fit the locations. change as neccesary 
        }
        else {
            Player.transform.rotation = new(0, 0, 0, 0);
        }
        Player.transform.position = Flags[i].transform.position; //Teleports player to hotspot
        if (Dialogue) {
            SeagullSpeaking.clip = Seagull_Dialogues[i]; //Sets correct dialogue for seagull
            SeagullSpeaking.Play(0); //Seagull starts yapping
        }
        SceneActivity(i);
        
    }

    public void SceneActivity(int i) { //Activates scripts related to each scene
        switch (i) {
            case 0:
                EnableActivity(ExploreActivity, true);
                EnableActivity(EndindActivity, false);
                break;
            case 1:
                EnableActivity(WorkerActivity, true);
                EnableActivity(ExploreActivity, false);
                break;
            case 2:
                EnableActivity(WheelActivity, true);
                EnableActivity(WorkerActivity, false);
                break;
            case 3:
                EnableActivity(ClimbingActivity, true);
                EnableActivity(WheelActivity, false);
                break;
            case 4:
                EnableActivity(SpottingActivity, true);
                EnableActivity(ClimbingActivity, false);
                break;
            case 5:
                EnableActivity(EndindActivity, true);
                Blurs.SetBool("End", true);
                EnableActivity(SpottingActivity, false);
                break;
        }
    }

    //<<All activities, these functions activate objects that contain scripts and are managed by SceneActivity, if you want to change activity code change it from the scripts inside those objects>>
    private void Explore(bool active) {
        ExploreActivity.SetActive(active);
        Debug.Log("<color=green>Explore active</color> <b><size= 15>" + active + "</size></b>");
    }

    public void EnableActivity(GameObject ActivityObject, bool active) {
        ActivityObject.SetActive(active);
        Debug.Log("<color=green>"+ ActivityObject.name + " active</color> <b>" + active + "</b>");
    }
    private void Worker(bool active) {
        WorkerActivity.SetActive(active);
        Debug.Log("<color=green>Worker active</color> <b><size= 15>" + active + "</size></b>");
    }
    private void Wheel(bool active) {
        WheelActivity.SetActive(active);
        Debug.Log("<color=green>Wheel active</color> <b><size= 15>" + active + "</size></b>");
    }
    private void Climb(bool active) {
        ClimbingActivity.SetActive(active);
        Debug.Log("<color=green>Climbing active</color> <b><size= 15>" + active + "</size></b>");
    }
    private void SpotShips(bool active) {
        SpottingActivity.SetActive(active);
        Debug.Log("<color=green>Spotting active</color> <b><size= 15>" + active + "</size></b>");
    }
    private void Ending(bool active) {
        EndindActivity.SetActive(active);
        Debug.Log("<color=green>Ending active</color> <b><size= 15>" + active + "</size></b>");
    }
    //<<End of activities>>
    void Start()
    {
        EnterScene(i,Dialogue);
        i++;
        Blurs.SetBool("End", false);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Timer) {
            TimeUntillNextScene -= Time.deltaTime;
            if (TimeUntillNextScene <= 0) {
                if (i >= 6) {
                    i = 0;
                    Debug.Log("reset"); //Change this to exit in final build
                }
                else {
                    EnterScene(i,Dialogue);
                    i++;
                }
                Debug.Log("<color=red>Timeout</color>");
            }
        }

    }
}
