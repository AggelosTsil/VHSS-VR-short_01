using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Hotspot : MonoBehaviour {

    public static HotspotManager hotspotManager;
    protected InfoElement[] elements;
    public enum State { none, firstAnimation, audioIntro, explore, interact, navigate };
    private State currentState = State.none;
    public bool hasFirstAnimation = true, hasExploration = true, hasInteraction = true, hasNavigation = true;
    private float exploreTimeout = 40;
    private float lastItemOpenedTimeout = 5;
    private float navigateTimeout = 30;
    private float interactTimeout = 400; //used to be 40
    public Hotspot() {
        
    }

    public abstract void Init();
   
    public void Update() { 
        switch(currentState) {
            case State.firstAnimation: //AGG&LOUTSAS modding ----- To animation me ton glaro na se ksipnaei
                if(hotspotManager.playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                    hotspotManager.playerAnimator.enabled = false;
                    hotspotManager.StatusFinished();
                }
                break;
            case State.audioIntro: //A&L ------ To analogo audiobite
                if (hotspotManager.source.isPlaying == false) {
                    hotspotManager.StatusFinished();
                }
                break;
            case State.explore: //A&L ------- Oso ginetai na einai active
                exploreTimeout -= Time.deltaTime;
                if (exploreTimeout < 0) {
                    StopExploration();
                    hotspotManager.StatusFinished();
                    break;
                }
                if (InfoElement.elementsOpen== elements.Length) { 
                    lastItemOpenedTimeout -= Time.deltaTime;
                    if (lastItemOpenedTimeout < 0) {
                        StopExploration();
                        hotspotManager.StatusFinished();
                    }
                }
                break;
            case State.interact:
                interactTimeout -= Time.deltaTime;
                if (interactTimeout < 0) {
                    hotspotManager.StatusFinished();
                }
                break;
            case State.navigate:
                navigateTimeout -= Time.deltaTime;
                if(navigateTimeout < 0) {
                    hotspotManager.ReachedDestination();
                }
                break;
        }
    }

    protected IEnumerator ShowTimedLabel(float waitTime, string labelName, float timeout) {
        yield return new WaitForSeconds(waitTime);
        if (currentState== State.audioIntro) 
            hotspotManager.infoElements[labelName].PopUp(timeout);
    }

    protected IEnumerator ShowHelper(float waitTime, string helperName, bool active) {
        yield return new WaitForSeconds(waitTime);
        hotspotManager.sceneElements[helperName].SetActive(active);
    }

    public abstract void InitState(State newState);
    public abstract void FinishState(State currentState);
    public void Popup(int num, float duration) {
        elements[num].PopUp(duration);
    }

    public virtual void ActiveExploration() {
        InfoElement.elementsOpen = 0;
        foreach (InfoElement ie in elements) {
            ie.SetLabelStatus(InfoElement.Status.infoPoint);
        }
    }

    public virtual void StopExploration() {
        Player.GetPlayer().SetState(Player.State.NO_INTERACTION);
        foreach (InfoElement ie in elements) {
            ie.SetLabelStatus(InfoElement.Status.hidden);
            ie.gameObject.SetActive(false);
        }
    }

    public void DebugSetState(int num) {
        currentState = (State)num;
        NextState();
    }

    public void Skip() {
        switch (currentState) {
            case State.firstAnimation:
                hotspotManager.playerAnimator.enabled = false;
                hotspotManager.ResetPlayerPos();
                hotspotManager.StatusFinished();
                break;
            case State.audioIntro:
                hotspotManager.source.Stop();
                break;
            case State.explore:
                StopExploration();
                hotspotManager.StatusFinished();
                break;
        }
    }
    public bool NextState() {
        FinishState(currentState);

        int newStateNum = (int)currentState + 1;
        if (newStateNum > (int)State.navigate) return false;

        currentState = (State)newStateNum;

        switch (currentState) {
            case State.firstAnimation:
                if (!hasFirstAnimation) return NextState();
                Player.GetPlayer().SetState(Player.State.NO_INTERACTION);
                break;
            case State.audioIntro:
                Player.GetPlayer().SetState(Player.State.NO_INTERACTION);
                PlayAudio("init");
                break;
            case State.explore:
                if (!hasExploration) return NextState();
                Player.GetPlayer().SetState(Player.State.EXPLORE);
                ActiveExploration();
                break;
            case State.interact:
                if (!hasInteraction) return NextState();
                PlayAudio("interact");
                Player.GetPlayer().SetState(Player.State.INTERACT);
                break;
            case State.navigate:
                if (!hasNavigation) return NextState();
                PlayAudio("nav");
                Player.GetPlayer().SetState(Player.State.NAVIGATE);
                hotspotManager.PrepareNextTeleport();
                hotspotManager.ShowAuxiliaryHotspots(true);
                break;
        }
        InitState(currentState);
        Debug.Log("current state: " + currentState);
        return true;
    }

    protected void PlayAudio(string phase) {
        AudioClip intro = Resources.Load<AudioClip>(LangManager.lang + "/audio-"+phase+"-" + gameObject.name);
        hotspotManager.source.clip = intro;
        hotspotManager.source.Play();
    }

    public void PrepareForTeleport() {
        GetComponent<TeleportationAnchor>().enabled = true;
        GetComponent<TeleportTargetVisualization>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ReachedDestination() {
        hotspotManager.ReachedDestination();
    }

    public void TeleportFinished() {
        hotspotManager.StatusFinished();
        hotspotManager.ShowAuxiliaryHotspots(false);
        gameObject.SetActive(false);
    }
}

