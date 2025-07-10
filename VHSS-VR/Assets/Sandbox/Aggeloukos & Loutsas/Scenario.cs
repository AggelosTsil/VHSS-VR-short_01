using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public bool Timer; //Togles timer function  
    public float NextSceneTimer;//The time it takes for a scene to auto-skip
    public float TimeExplore;//The time left for the current scene
    public float TimeClimb;
    public float TimeSpot;
    public GameObject Player;
    public GameObject[] Flags;//Teleport hotspots
    public string FirstScene;
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
    
     public void EnterScene(string SceneName, bool Dialogue) { //Initiallises next scene <<NOTE ADD i++>>
        Debug.Log("<color=yellow>Entered Scene </color>" + SceneName);
        //TimeExplore = NextSceneTimer; //Initiallises/resets timer
        /*if (i <= 3) {
            Player.transform.rotation = new(0, 0, 0, 0); //Rotational changes are purely to fit the locations. change as neccesary 
        }
        else {
            Player.transform.rotation = new(0, 0, 0, 0);
        }*/
        //Player.transform.position = Flags[i].transform.position; //Teleports player to hotspot
       /* if (Dialogue) {
            SeagullSpeaking.clip = Seagull_Dialogues[i]; //Sets correct dialogue for seagull
            SeagullSpeaking.Play(0); //Seagull starts yapping
        }*/
        SceneActivity(SceneName);
        
    }

    public void SceneActivity(string ActivityName) { //Activates scripts related to each scene
        
        switch (ActivityName) {
            case "Explore":
                EnableActivity(ExploreActivity, true);
               // EnableActivity(EndindActivity, false);
                break;
            case "Climbing":
                EnableActivity(ClimbingActivity, true);
                EnableActivity(ExploreActivity, false);
                break;
            case "Worker":
                EnableActivity(WorkerActivity, true);
                EnableActivity(ExploreActivity, false);
                break;
            case "Wheel":
                EnableActivity(WheelActivity, true);
                EnableActivity(ExploreActivity, false);
                break;
            case "Spotting":
                EnableActivity(SpottingActivity, true);
                EnableActivity(ClimbingActivity, false);
                break;
            case "Ending":
                EnableActivity(EndindActivity, true);
                Blurs.SetBool("End", true); //for the animation
                EnableActivity(SpottingActivity, false);
                break;
        }
    }

    public void EnableActivity(GameObject ActivityObject, bool active) {
        ActivityObject.SetActive(active);
        Debug.Log("<color=green>"+ ActivityObject.name + " active</color> <b>" + active + "</b>");
    }
    //<<End of activities>>
    void Start()
    {
        EnterScene(FirstScene,Dialogue);
        Blurs.SetBool("End", false);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Timer && (TimeExplore > 0)) {
            TimeExplore -= Time.deltaTime;
            if (TimeExplore <= 0) {
                Debug.Log("<color=red>Timeout</color>");
            }
        }

    }
}
