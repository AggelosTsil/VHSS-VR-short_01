using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSWheel : Hotspot {

    public Transform InteractionPoint;

    public override void Init() {
        elements = hotspotManager.GetInfoElements(new string[]
        { "roda", "labes", "tabouki", "pyxidothiki" }
        );

        foreach (InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }
    }

    public override void InitState(State newState) {
        switch (newState) {
            case State.audioIntro:
                StartCoroutine(ShowTimedLabel(3.5f, "roda", 5));
                StartCoroutine(ShowTimedLabel(6f, "labes", 5));
                StartCoroutine(ShowTimedLabel(22f, "pyxidothiki", 5));
                break;
            case State.explore:
                break;
            case State.interact:
                StartCoroutine(TakeInteractionPos(1f));
                StartCoroutine(ShowHelper(2f, "HLPwheel", true));
                break;
            case State.navigate:
                break;
        }
    }

    public override void FinishState(State currentState) {
        switch (currentState) {
            case State.interact:
                StartCoroutine(ReturnToNormalPos(1.5f));
                StartCoroutine(ShowHelper(1f, "HLPwheel", false));
                break;
        }
    }

    IEnumerator TakeInteractionPos(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        hotspotManager.player.position = InteractionPoint.position;
        hotspotManager.player.rotation = InteractionPoint.rotation;
    }

    IEnumerator ReturnToNormalPos(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        hotspotManager.player.position = transform.position;
        hotspotManager.player.rotation = transform.rotation;
    }
}
