using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class HotspotManager : MonoBehaviour {

    // <ganast 20230702>
    [SerializeField]
    private InputActionProperty restartSceneAction;
    // </ganast>

    public Transform player;
    //public Hotspot activeHotspot;

    public Transform hotspotsFolder; //player destinations
    public Transform infoElementsFolder; // labels of the ship
    public GameObject auxiliaryHotspots;
    public GameObject[] sceneElementsList; //other objects of the scene that show / hide / change between hotspots

    public List<Hotspot> hotspots;
    public Dictionary<string, InfoElement> infoElements;
    public Dictionary<string, GameObject> sceneElements;
    public Animator playerAnimator { get; private set; }
    public AudioSource source;

    [Tooltip("entrance=0, worker, bowsails, wheel, stern")]
    public int currentHotspot = 0;

    [Tooltip("firstAnimation=0, audioIntro, explore, interact, navigate")]
    public int currentState = 0;

    private bool finished = false;
    private Vector3 playerStartPos = new Vector3(2.6170001f, 0.00499999989f, -2.57500005f);

    // <ganast 20230704>
    [SerializeField]
    private float hardRestartPressLength;
    // </ganast>

    // <ganast 20230702>
    protected void OnRestartActionStarted(InputAction.CallbackContext iac) {
    }
    // </ganast>

    // <ganast 20230702>
    protected void OnRestartActionPerformed(InputAction.CallbackContext iac) {

        bool hard = iac.duration > hardRestartPressLength;

        Debug.LogFormat("[HotspotManager] OnRestartActionPerformed type={0}", hard ? "hard" : "soft");

        if (hard) {
            Debug.Log("HARD RESTART!!");
            RestartApp();
        }
        else {
            Debug.Log("SOFT RESTART!!");
            if (finished) {
                RestartApp();
            }
            else {
                hotspots[currentHotspot].Skip();
            }
        }
    }
    // </ganast>

    // <ganast 20230702>
    protected void OnRestartActionCanceled(InputAction.CallbackContext iac) {
        // TODO
    }
    // </ganast>

    void Awake() {
        // <ganast 20230702>
        restartSceneAction.action.started += OnRestartActionStarted;
        restartSceneAction.action.performed += OnRestartActionPerformed;
        restartSceneAction.action.canceled += OnRestartActionCanceled;
    }
    // </ganast>

    // Start is called before the first frame update
    void Start() {
        Hotspot.hotspotManager = this;
        
        hotspots = new List<Hotspot>();
        for(int i=0; i<hotspotsFolder.childCount;i++)
            hotspots.Add(hotspotsFolder.GetChild(i).GetComponent<Hotspot>());
        infoElements = new Dictionary<string, InfoElement>();
        for (int i=0; i<infoElementsFolder.childCount;i++)
            infoElements.Add(infoElementsFolder.GetChild(i).name, infoElementsFolder.GetChild(i).GetComponent<InfoElement>());
        sceneElements = new Dictionary<string, GameObject>();
        foreach (GameObject g in sceneElementsList)
            sceneElements.Add(g.name, g);
        playerAnimator = player.gameObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        SetActiveHotspot(hotspots[currentHotspot]);
        StartCoroutine(LateStart(1));
    }
    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        auxiliaryHotspots.SetActive(false);
        hotspots[currentHotspot].DebugSetState(currentState);
    }

    public bool NextHotspot() {
        currentHotspot++;
        if(currentHotspot>=hotspots.Count) {
            return false;
        }
        SetActiveHotspot(hotspots[currentHotspot]);
        hotspots[currentHotspot].NextState();
        return true;
    }
    public void SetActiveHotspot(Hotspot hotspot) {
        Debug.Log("Active Hotspot: " + hotspot.name);
        player.position = hotspot.transform.position;
        player.rotation = hotspot.transform.rotation;
        hotspot.Init();
    }

    public InfoElement[] GetInfoElements(string[] names) {
        InfoElement[] elements = new InfoElement[names.Length];
        for(int i=0;i<names.Length;i++) {
            elements[i] = infoElements[names[i]];
        }
        return elements;
    }

    public void StatusFinished() {
        if (!hotspots[currentHotspot].NextState()) {
            if (!NextHotspot()) {
                Debug.Log(">>>> game finished!");
                finished = true;
            }
        }
    }

    // Update is called once per frame
    void Update() {
       if (Keyboard.current[Key.A].wasPressedThisFrame) {
            StatusFinished();
        }
        if (Keyboard.current[Key.B].wasPressedThisFrame) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
    }

    public void RestartApp() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReachedDestination() {
        hotspots[currentHotspot+1].gameObject.GetComponent<TeleportationAnchor>().enabled = false;
        hotspots[currentHotspot + 1].gameObject.GetComponent<TeleportTargetVisualization>().enabled = false;
        for (int i = 0; i < hotspots[currentHotspot + 1].transform.childCount; i++) {
            hotspots[currentHotspot + 1].transform.GetChild(i).gameObject.SetActive(false);
        }
        hotspots[currentHotspot].TeleportFinished();
    }

    public void PrepareNextTeleport() {
        hotspots[currentHotspot + 1].PrepareForTeleport();
    }

    public void ShowAuxiliaryHotspots(bool show) {
        auxiliaryHotspots.SetActive(show);
    }

    public void ResetPlayerPos() {
        player.localPosition = playerStartPos;
    }
}
