using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSWorker : Hotspot {

    public Transform InteractionPoint;

    public override void Init() {
        elements = hotspotManager.GetInfoElements(new string[]
        { "manaveles", "kamptiras", "peritroxon" }
        );

        foreach (InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }

    }

    public override void InitState(State newState) {
        switch (newState) {
            case State.audioIntro:
                StartCoroutine(ShowHelper(1f, "HLPteleport", false));
                StartCoroutine(ShowTimedLabel(14f, "peritroxon", 5));
                StartCoroutine(ShowTimedLabel(24f, "manaveles", 5));
                break;
            case State.explore:
                break;
            case State.interact:
                StartCoroutine(TakeInteractionPos(1f));
                StartCoroutine(ShowHelper(2f, "HLPworker", true));
                break;
            case State.navigate:
                break;
        }
    }

    public override void FinishState(State currentState) {
        switch (currentState) {
            case State.interact:
                StartCoroutine(ReturnToNormalPos(1.5f));
                StartCoroutine(ShowHelper(1f, "HLPworker", false));
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
