using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSFinale : Hotspot {
    public Transform playerTransform;
    public GameObject finalBlur;
    public override void Init() {
        hotspotManager.player.transform.parent = null;
        hotspotManager.player.transform.localScale = new Vector3(10, 10, 10);

        elements = hotspotManager.GetInfoElements(new string[]
        { "trigkos", "ploriagapia", "prymiagapia", "ploriosmpafigkos", "prymiosmpafigkos",
        "plorioskontra", "prymioskontra", "akroproro"
        }
        );

        foreach (InfoElement ie in elements) {
            ie.gameObject.SetActive(true);
        }
    }

    public override void InitState(State newState) {
        switch (newState) {
            case State.audioIntro:
                StartCoroutine(ShowTimedLabel(7f, "trigkos", 10));
                StartCoroutine(ShowTimedLabel(8f, "ploriagapia", 10));
                StartCoroutine(ShowTimedLabel(9f, "prymiagapia", 10));
                StartCoroutine(ShowTimedLabel(10f, "ploriosmpafigkos", 10));
                StartCoroutine(ShowTimedLabel(11f, "prymiosmpafigkos", 10));
                StartCoroutine(ShowTimedLabel(12f, "plorioskontra", 10));
                StartCoroutine(ShowTimedLabel(13f, "prymioskontra", 10));
                StartCoroutine(ShowTimedLabel(14f, "akroproro", 10));
                //------
                StartCoroutine(HideElement(20, "Seagulls"));
                StartCoroutine(HideElement(30, "Environment"));
                StartCoroutine(ChangeBackground(30));
                StartCoroutine(HideElement(32, "Ocean_TransparentQueue"));
                StartCoroutine(StopPhysics(35));
                StartCoroutine(HideElement(55, "Ship"));
                StartCoroutine(HideElement(56, "blur"));
                break;
        }
    }


    public override void FinishState(State currentState) {
        //Debug.Log("finishing state " + currentState);
        switch (currentState) {
            case State.audioIntro:
                finalBlur.SetActive(true);
                break;
        }   
    }

    IEnumerator HideElement(float waitTime, string name) {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find(name).SetActive(false);
    }

    IEnumerator ChangeBackground(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
    }

    IEnumerator StopPhysics(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Ship").GetComponent<Rigidbody>().isKinematic = true;
    }
}