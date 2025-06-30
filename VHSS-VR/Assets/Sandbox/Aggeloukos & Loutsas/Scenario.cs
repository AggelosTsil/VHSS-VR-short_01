using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public float NextSceneTimer = 3;//The time it takes for a scene to auto-skip
    public float TimeUntillNextScene;//The time left for the current scene
    public GameObject Player;
    public GameObject[] Flags;//Teleport hotspots
    public int i = 0;//Good ol' reliable
    public AudioSource SeagullSpeaking; //The source from where you hear the seagul speak
    public AudioClip[] Seagull_Dialogues; //The different things the seagull says


     public void EnterScene(int i) { //Initiallises next scene <<NOTE ADD i++>>
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
        SeagullSpeaking.Play(0); //Seagull starts yapping
    }
    void Start()
    {
        EnterScene(i);
        i++;
    }
   

    // Update is called once per frame
    void Update()
    {
        TimeUntillNextScene -= Time.deltaTime;
        if (TimeUntillNextScene <= 0) {
            if (i >= 6) {
                i = 0;
                Debug.Log("reset"); //Change this to exit in final build
            }
            else { 
                EnterScene(i);
                i++;
            }  
            Debug.Log("<color=green>Timeout</color>");
        }

    }
}
