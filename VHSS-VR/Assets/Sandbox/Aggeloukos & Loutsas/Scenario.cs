using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {
    public bool Timer; //Toggles timer function  
    public DirectionHelper directionHelper;
    public float NextSceneTimer;//The time it takes for a scene to auto-skip
    public float TimeExplore;//The time left for the current scene
    public float TimeClimb;
    public float TimeSpot;
    public GameObject Player;
    public GameObject[] Flags;//Teleport hotspots
    public string FirstScene;
    public AudioSource SeagullSpeaking; //The source from where you hear the seagul speak
    public bool english = false;
    public AudioClip[] Seagull_Diaologues_Greek;
    public AudioClip[] Seagull_Diaologues_English;
    public AudioClip[] Seagull_Dialogues; //The different things the seagull says
    public bool Dialogue;//Shuts up the seagull
    //<<Activity Scripts>>
    public GameObject LangActivity;
    public GameObject IntroActivity;
    public GameObject ExploreActivity;
    public GameObject WorkerActivity;
    public GameObject WheelActivity;
    public GameObject ClimbingActivity;
    public GameObject SpottingActivity;
    public GameObject EndindActivity;
    public Animator Blurs;
    public bool ExplorationDialogueHasntPlayed;
    public bool WorkerDialogueHasntPlayed;
    public bool WheelDialogueHasntPlayed;
    public bool SpottingDialogueHasntPlayed;
    public bool ClimbingDialogueHasntPlayed;
    public bool promptHasntPlayed;
    public bool gunHasntPlayed;
    public bool EndingHappened; //If the ending happended, will be used in the update to initiate the fade out
    public bool nogun;
    public bool OofPlayed;
    public GameObject HS;
    public Exploration Exploration;

    public Playthings Playthings;

    public Spotting Spotting;

    public GameObject credits;

    public MusicManager MusicManager;

    public bool Phase2VC;
    public GameObject[] ships;

    


    //<<End of Activity Scripts>>

    public void EnterScene(string SceneName, bool Dialogue) { //Initiallises next scene <<NOTE ADD i++>>
        Debug.Log("<color=yellow>Entered Scene </color>" + SceneName);
        
        SceneActivity(SceneName);
    }

    public void SceneActivity(string ActivityName) { //Activates scripts related to each scene

        switch (ActivityName) {
            case "Lang":
                EnableActivity(LangActivity, true);
                EnableActivity(IntroActivity, false);
                EnableActivity(ExploreActivity, false);
                EnableActivity(WorkerActivity, false);
                EnableActivity(WheelActivity, false);
                EnableActivity(SpottingActivity, false);
                break;
            case "Intro":
                EnableActivity(LangActivity, false);
                EnableActivity(IntroActivity, true);
                EnableActivity(ExploreActivity, false);
                EnableActivity(WorkerActivity, false);
                EnableActivity(WheelActivity, false);
                EnableActivity(SpottingActivity, false);
                SeagullSpeaking.clip = Seagull_Dialogues[5]; //Sets correct dialogue for seagull
                
                break;
            case "Explore":
                EnableActivity(IntroActivity, false);
                EnableActivity(ExploreActivity, true);
                EnableActivity(WorkerActivity, false);
                EnableActivity(WheelActivity, false);
                EnableActivity(SpottingActivity, false);

                if (ExplorationDialogueHasntPlayed && Dialogue) {
                    SeagullSpeaking.clip = Seagull_Dialogues[0]; //Sets correct dialogue for seagull
                    Exploration.BillboardON();
                    SeagullSpeaking.Play(44100); //Seagull starts yapping
                    ExplorationDialogueHasntPlayed = false;
                }
                break;
            case "Worker":
                EnableActivity(WorkerActivity, true);
                EnableActivity(ExploreActivity, false);

                SeagullSpeaking.clip = Seagull_Dialogues[1];
                if (WorkerDialogueHasntPlayed && Dialogue) {
                    SeagullSpeaking.Play(0);
                    WorkerDialogueHasntPlayed = false;
                }
                break;
            case "Wheel":
                EnableActivity(WheelActivity, true);
                EnableActivity(ExploreActivity, false);

                SeagullSpeaking.clip = Seagull_Dialogues[2];
                if (WheelDialogueHasntPlayed && Dialogue) {
                    SeagullSpeaking.Play(0);
                    WheelDialogueHasntPlayed = false;
                }
                break;
            case "Climbing":
                EnableActivity(ClimbingActivity, true);
                EnableActivity(ExploreActivity, false);
                nogun = true;
                SeagullSpeaking.clip = Seagull_Dialogues[3];
                if (ClimbingDialogueHasntPlayed && Dialogue) {
                    SeagullSpeaking.Play(0);
                    ClimbingDialogueHasntPlayed = false;
                }
                break;
            case "Spotting":
                EnableActivity(SpottingActivity, true);
                EnableActivity(ClimbingActivity, false);
                EnableActivity(ExploreActivity, false);
                nogun = true;
                SeagullSpeaking.clip = Seagull_Dialogues[7];
                if (SpottingDialogueHasntPlayed && Dialogue) {
                    SeagullSpeaking.Play(0);
                    SpottingDialogueHasntPlayed = false;
                }
                break;
            case "Ending":
                EnableActivity(EndindActivity, true);
                EnableActivity(SpottingActivity, false);
                EnableActivity(ExploreActivity, false);
                EnableActivity(ClimbingActivity, false);
                SeagullSpeaking.clip = Seagull_Dialogues[4];
                if (Dialogue) {
                    Debug.Log("Ending is speaking");
                    SeagullSpeaking.Play(0);
                }
                EndingHappened = true; //will be used in the update to initiate the fade out

                break;
        }
    }

    public void EnableActivity(GameObject ActivityObject, bool active) {
        ActivityObject.SetActive(active);
        Debug.Log("<color=green>" + ActivityObject.name + " active</color> <b>" + active + "</b>");
    }
    //<<End of activities>>
    void Start() {
        EnterScene(FirstScene, Dialogue);
        Blurs.SetBool("End", false);
    }

    // Update is called once per frame
    void Update() {
        if (Timer && (TimeExplore > 0) && !LangActivity.activeSelf) {
            TimeExplore -= Time.deltaTime;
        }
        
        else if (TimeExplore <= 0) {
            if (!Exploration.Phase2bool) {
                MusicManager.playphase2();
                Phase2IntroVC();
                Exploration.Phase2();
            }
           
           
        }
        Debug.Log("<color=red>Timeout</color>");
        //Seagull Exploration 2 dialogue
        if (Timer && (TimeExplore <= 0) && (TimeSpot > 0)) {
            TimeSpot -= Time.deltaTime;
        } else if (Timer && (TimeExplore <= 0) && (TimeSpot <= 0) && !EndingHappened) {
            EnterScene("Ending", Dialogue);
            StartCoroutine(MusicManager.FadeOut(MusicManager.AS ,1f));
        }

        if ((!Playthings.PistolRight.activeSelf && !Playthings.PistolLeft.activeSelf) || LangActivity.activeSelf)
        {
            HS.SetActive(false);
            Debug.Log("HS closed");
        }
        else 
        {
            HS.SetActive(true);
            Debug.Log("HS Open");
            if (Exploration.Bill.activeSelf) //closes pick up pistol billboard
            {
                Exploration.BillboardOFF();
            }
            
        }


        //All ships spotted (don't judge, I had a long day)
        if (!ships[0].activeSelf && !ships[1].activeSelf && !ships[2].activeSelf && !SeagullSpeaking.isPlaying && !Phase2VC && !OofPlayed)
        {
            OofVC();
        }



    }

    private void LateUpdate() {

        if (EndingHappened && !SeagullSpeaking.isPlaying) //For the fade out
        {
            Debug.Log("Ending Happening wow 2");
            Blurs.SetBool("End", true);
            credits.SetActive(true);
        }
    }

    public void LangSelection() {
        if (!english) {
            for (int i = 0; i < Seagull_Dialogues.Length; i++) {
                Seagull_Dialogues[i] = Seagull_Diaologues_Greek[i];
            }
        }
        else {
            for (int i = 0; i < Seagull_Dialogues.Length; i++) {
                Seagull_Dialogues[i] = Seagull_Diaologues_English[i];

            }
        }
    }
    public void PromptVoiceclip()
    {
        SeagullSpeaking.clip = Seagull_Dialogues[7];
        if (promptHasntPlayed && Dialogue)
        {
            SeagullSpeaking.Play(0);
            promptHasntPlayed = false;
        }
    }

    public void GunVoiceclip()
    {
        
        if (gunHasntPlayed && Dialogue)
        {
            
            SeagullSpeaking.clip = Seagull_Dialogues[6];
            SeagullSpeaking.Play(0);
            gunHasntPlayed = false;
            
        }
    }

    public void FregataVC()
    {
        SeagullSpeaking.clip = Seagull_Dialogues[8];
        if (Dialogue)
        {
            SeagullSpeaking.Play(0);
           
        }

    }
    public void BrigiVC()
    {
        SeagullSpeaking.clip = Seagull_Dialogues[11];
        if (Dialogue)
        {
            SeagullSpeaking.Play(0);
           
        }

    }
    public void PyrpolikoVC()
    {
        SeagullSpeaking.clip = Seagull_Dialogues[10];
        if (Dialogue)
        {
            SeagullSpeaking.Play(0);
          
        }

    }

    public void Phase2IntroVC() {

        if (Phase2VC && Dialogue) {
            SeagullSpeaking.clip = Seagull_Dialogues[9];
            SeagullSpeaking.Play(0);
            Phase2VC = false;
        }

    }

    public void OofVC() {

        SeagullSpeaking.clip = Seagull_Dialogues[12];
        if (Dialogue) {
            SeagullSpeaking.Play(44100);
        }
        OofPlayed = true;
    }
}

