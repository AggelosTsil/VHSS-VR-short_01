using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSStern : Hotspot {

    public override void Init() {
        elements = hotspotManager.GetInfoElements(new string[]
        { "kaponi", "bouma", "ranta", "palagko" }
        );

        foreach (InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }
    }

    public override void InitState(State newState) {
        switch (newState) {
            case State.audioIntro:
                StartCoroutine(ShowTimedLabel(2.5f, "bouma", 5));
                StartCoroutine(ShowTimedLabel(13f, "ranta", 5));
                StartCoroutine(ShowTimedLabel(19f, "palagko", 5));
                break;
            case State.explore:
                break;
            case State.interact:
                break;
            case State.navigate:
                break;
        }
    }

    public override void FinishState(State currentState) {

    }
}
