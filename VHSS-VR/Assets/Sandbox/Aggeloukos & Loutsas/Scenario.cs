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

    //<<End of Activity Scripts>>
    

   


     public void EnterScene(int i, bool Dialogue) { //Initiallises next scene <<NOTE ADD i++>>
        Debug.Log("<color=yellow>Entered Scene </color>" + Flags[i]);
        TimeUntillNextScene = NextSceneTimer; //Initiallises/resets timer
        if (i <= 3) {
            Player.transform.rotation = new(0, 0, 0, 0); //Rotational changes are purely to fit the locations. change as neccesary 
        }
        else {
            Player.transform.rotation = new(0, 90, 0, 0);
        }
        Player.transform.position = Flags[i].transform.position; //Teleports player to hotspot
        SeagullSpeaking.clip = Seagull_Dialogues[i]; //Sets correct dialogue for seagull
        if (Dialogue) {
            SeagullSpeaking.Play(0); //Seagull starts yapping
        }
        SceneActivity(i);
        
    }

    public void SceneActivity(int i) { //Activates scripts related to each scene
        switch (i) {
            case 0: 
                Explore(true);
                Ending(false);
                break;
            case 1: 
                Worker(true);
                Explore(false);
                break;
            case 2: 
                Wheel(true);
                Worker(false);
                break;
            case 3: 
                Climb(true);
                Wheel(false);
                break;
            case 4: 
                SpotShips(true);
                Climb(false);
                break;
            case 5: 
                Ending(true);
                SpotShips(false);
                break;
        }
    }

    //<<All activities, these functions activate objects that contain scripts and are managed by SceneActivity, if you want to change activity code change it from the scripts inside those objects>>
    public void Explore(bool active) {
        ExploreActivity.SetActive(active);
        Debug.Log("<color=green>Explore active</color> <b><size= 15>" + active + "</size></b>");
    }
    public void Worker(bool active) {
        WorkerActivity.SetActive(active);
        Debug.Log("<color=green>Worker active</color> <b><size= 15>" + active + "</size></b>");
    }
    public void Wheel(bool active) {
        WheelActivity.SetActive(active);
        Debug.Log("<color=green>Wheel active</color> <b><size= 15>" + active + "</size></b>");
    }
    public void Climb(bool active) {
        ClimbingActivity.SetActive(active);
        Debug.Log("<color=green>Climbing active</color> <b><size= 15>" + active + "</size></b>");
    }
    public void SpotShips(bool active) {
        SpottingActivity.SetActive(active);
        Debug.Log("<color=green>Spotting active</color> <b><size= 15>" + active + "</size></b>");
    }
    public void Ending(bool active) {
        EndindActivity.SetActive(active);
        Debug.Log("<color=green>Ending active</color> <b><size= 15>" + active + "</size></b>");
    }
    //<<End of activities>>
    void Start()
    {
        EnterScene(i,Dialogue);
        i++;
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
