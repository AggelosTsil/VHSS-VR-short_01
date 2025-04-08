using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HSEntrance : Hotspot {

    public override void Init() {
        elements = hotspotManager.GetInfoElements(new string[] 
        { "xartia", "ergatis", "maistra", "skota", "kavilies", "provolos", "antena", "feggitis" }
        );

        foreach(InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }
        //hotspotManager.infoElements["flokos"].gameObject.SetActive(true);
        //hotspotManager.infoElements["bouma"].gameObject.SetActive(true);
    }

    public override void InitState(State newState) {
        switch(newState) {
            case State.firstAnimation:
                hotspotManager.playerAnimator.enabled = true;
                hotspotManager.playerAnimator.Play("playerIntro");
                break;
            case State.audioIntro:
                StartCoroutine(ShowTimedLabel(16f, "provolos", 5));
                //StartCoroutine(ShowTimedLabel(23f, "flokos", 5));
                //StartCoroutine(ShowTimedLabel(30f, "bouma", 5));
                StartCoroutine(ShowTimedLabel(38f, "antena", 5));
                break;
            case State.explore:
                StartCoroutine(ShowHelper(1f, "HLPinfo", true));
                break;
            case State.interact:
                break;
            case State.navigate:
                StartCoroutine(ShowHelper(1f, "HLPteleport", true));
                break;
        }
    }


    public override void FinishState(State currentState) {
        switch (currentState) {
            case State.explore:
                StartCoroutine(ShowHelper(1f, "HLPinfo", false));
                break;         
        }
    }

    /*public override void Update() {
        if (Keyboard.current[Key.A].wasPressedThisFrame) {
            elements[0].SetLabelStatus(InfoElement.Status.infoPointClicked);
        }
        if (Keyboard.current[Key.B].wasPressedThisFrame) {
            elements[1].PopUp(2);
        }
        if (Keyboard.current[Key.C].wasPressedThisFrame) {
            ActiveExploration();
        }
        if (Keyboard.current[Key.D].wasPressedThisFrame) {
            StopExploration();
        }
    }*/
}
