using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSBowsails : Hotspot {

    public override void Init() {
        elements = hotspotManager.GetInfoElements(new string[]
        { "agkyra", "xylinokaponi", "protonos", "venta", "kontraflokos", "flokos", "tourketina" }
        );

        foreach (InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }
    }

    public override void InitState(State newState) {
        switch (newState) {
            case State.audioIntro:
                StartCoroutine(ShowTimedLabel(2f, "agkyra", 5));
                StartCoroutine(ShowTimedLabel(11f, "xylinokaponi", 5));
                StartCoroutine(ShowTimedLabel(24f, "tourketina", 5));
                StartCoroutine(ShowTimedLabel(24.7f, "flokos", 5));
                StartCoroutine(ShowTimedLabel(25.4f, "kontraflokos", 5));
                StartCoroutine(ShowTimedLabel(36f, "venta", 5));
                StartCoroutine(ShowTimedLabel(40f, "protonos", 5));
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
