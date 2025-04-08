using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LangManager : MonoBehaviour {
    public GameObject shipMesh, flags, hotspots;
    public static string lang;

    // Start is called before the first frame update
    void Start() {
        shipMesh.SetActive(false);
        flags.SetActive(true);
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Player.GetPlayer().SetState(Player.State.EXPLORE);
        hotspots.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Keyboard.current[Key.D].wasPressedThisFrame) {
            
            //StartApp();
        }
    }

    private void StartApp() {
        Player.GetPlayer().SetState(Player.State.NO_INTERACTION);
        shipMesh.SetActive(true);
        flags.SetActive(false);
        GetComponent<HotspotManager>().enabled = true;
    }

    public void SetLanguage(string lang) {
        LangManager.lang = lang;
        StartApp();
    }

}
