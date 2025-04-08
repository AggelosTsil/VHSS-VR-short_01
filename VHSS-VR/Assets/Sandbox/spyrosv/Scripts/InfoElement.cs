using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfoElement : MonoBehaviour {
    // Start is called before the first frame update
    public Outline referringObjectOutline;
    private GameObject infoPoint;
    private GameObject infoText;
    private GameObject infoTextShort;
    public static InfoElement currentlyClicked;
    public static int elementsOpen;
    public static bool firstClick=false;

    public enum Status { hidden, popup, infoPoint, infoPointHover, infoPointClicked };
    private Status currentStatus;
    private float popupTimer;

    private bool opened = false;

    public void Start() {
        infoPoint = transform.GetChild(0).Find("InfoPoint").gameObject;
        if(LangManager.lang=="gr") {
            Debug.Log("GR!");
            infoText = transform.GetChild(0).Find("txtGr").gameObject;
            infoTextShort = transform.GetChild(0).Find("txtShortGr").gameObject;
        }
        else {
            Debug.Log("EN! "+LangManager.lang);
            infoText = transform.GetChild(0).Find("txtEn").gameObject;
            infoTextShort = transform.GetChild(0).Find("txtShortEn").gameObject;
        }
        SetLabelStatus(Status.hidden);
    }
    public void ShowOutline(bool show) {
        referringObjectOutline.enabled = show;
    }

    private void ShowInfoPoint(bool show) {
        infoPoint.SetActive(show);
    }

    private void ShowText(bool show, bool shortVersion=false) {
        if (shortVersion) {
            infoTextShort.SetActive(show);
        }
        else {
            infoText.SetActive(show);
        }
    }

    public void SetLabelStatus(Status status) {
        switch(status) {
            case Status.hidden: //nothing
                ShowInfoPoint(false);
                ShowOutline(false);
                ShowText(false, false);
                ShowText(false, true);
                break;
            case Status.popup: //outline, short text
                ShowInfoPoint(false);
                ShowOutline(true);
                ShowText(false, false);
                ShowText(true, true);
                break;
            case Status.infoPoint: //infopoint
                ShowInfoPoint(true);
                ShowOutline(false);
                ShowText(false, false);
                ShowText(false, true);
                break;
            case Status.infoPointHover: //infopoint, short text
                ShowInfoPoint(true);
                ShowOutline(false);
                ShowText(false, false);
                ShowText(true, true);
                break;
            case Status.infoPointClicked: //outline, infopoint, full text
                ShowInfoPoint(true);
                ShowOutline(true);
                ShowText(true, false);
                ShowText(false, true);
                break;
        }
        currentStatus = status;
    }

    public void PopUp(float timeout) {
        SetLabelStatus(Status.popup);
        popupTimer = timeout;   
    }

    public void HoverEnter() {
        if (currentlyClicked == this) return;
        SetLabelStatus(Status.infoPointHover);
    }

    public void HoverExit() {
        if (currentlyClicked == this) return;
        SetLabelStatus(Status.infoPoint);
    }

    public void Action() {
        if (currentlyClicked != this) {
            if (currentlyClicked != null)
                currentlyClicked.SetLabelStatus(Status.infoPoint);
            currentlyClicked = this;
            SetLabelStatus(Status.infoPointClicked);
            if(!opened) {
                opened = true;
                InfoElement.elementsOpen++;
            }
            if(!firstClick) {
                GameObject.Find("HLPinfo").SetActive(false);
                firstClick = true;
            }
        }
        else {
            currentlyClicked = null;
            SetLabelStatus(Status.infoPoint);
        }
    }

    public void Update() {
        switch(currentStatus) {
            case Status.popup:
                popupTimer -= Time.deltaTime;
                if (popupTimer < 0) SetLabelStatus(Status.hidden);
                break;
        }
    }

}
