using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionGameObjectSelector: MonoBehaviour {

    // TODO: temporarily only two gameobjects, extend for arbitrary
    // number of action-gameobjects pairs and provide UI through a
    // custom editor...

    [SerializeField]
    private GameObject idleGameObject;

    [SerializeField]
    private bool idleGameObjectActiveatable;

    [SerializeField]
    private GameObject primaryGameObject;

    [SerializeField]
    private InputActionProperty primaryGameObjectTrigger;

    [SerializeField]
    private bool primaryGameObjectActiveatable;

    [SerializeField]
    private BaseMonoBehaviourCondition[] primaryGameObjectConditions;

    [SerializeField]
    private GameObject secondaryGameObject;

    [SerializeField]
    private InputActionProperty secondaryGameObjectTrigger;

    [SerializeField]
    private bool secondaryGameObjectActiveatable;

    [SerializeField]
    private BaseMonoBehaviourCondition[] secondaryGameObjectConditions;

    private Dictionary<InputAction, bool> activationMap = new Dictionary<InputAction, bool>();

    private int isUpdateNeeded = -1;

    public bool checkConditions(BaseMonoBehaviourCondition[] conditions) {
        foreach (BaseMonoBehaviourCondition c in conditions) {
            if (!c.IsTrue()) {
                return false;
            }
        }
        return true;
    }

    public void Awake() {

        if (primaryGameObject != null) {
            activationMap.Add(primaryGameObjectTrigger.action, false);
            primaryGameObjectTrigger.action.started += OnActionStarted;
            primaryGameObjectTrigger.action.performed += OnActionPerformed;
            primaryGameObjectTrigger.action.canceled += OnActionCanceled;
        }

        if (secondaryGameObject != null) {
            activationMap.Add(secondaryGameObjectTrigger.action, false);
            secondaryGameObjectTrigger.action.started += OnActionStarted;
            secondaryGameObjectTrigger.action.performed += OnActionPerformed;
            secondaryGameObjectTrigger.action.canceled += OnActionCanceled;
        }

        isUpdateNeeded = 1;
    }

    public void Start() {

        idleGameObject.SetActive(true);

        if (primaryGameObject != null) {
            primaryGameObject.SetActive(false);
        }
        if (secondaryGameObject != null) {
            secondaryGameObject.SetActive(false);
        }
    }

    public void Update() {
        if (isUpdateNeeded > 0) {
            isUpdateNeeded--;
        }
        else if (isUpdateNeeded == 0) {
            // TODO: replace the following spaghetti a la madanasta with something the least bit
            // decent...
            if (primaryGameObject != null && checkConditions(primaryGameObjectConditions) && activationMap[primaryGameObjectTrigger.action]) {
                primaryGameObject.SetActive(primaryGameObjectActiveatable);
                if (secondaryGameObject != null && secondaryGameObject.activeSelf) {
                    secondaryGameObject.SetActive(false);
                }
                if (idleGameObject.activeSelf) {
                    idleGameObject.SetActive(false);
                }
            }
            else if (secondaryGameObject != null && checkConditions(secondaryGameObjectConditions) && activationMap[secondaryGameObjectTrigger.action]) {
                if (primaryGameObject != null && primaryGameObject.activeSelf) {
                    primaryGameObject.SetActive(false);
                }
                secondaryGameObject.SetActive(secondaryGameObjectActiveatable);
                if (idleGameObject.activeSelf) {
                    idleGameObject.SetActive(false);
                }
            }
            else {
                if (primaryGameObject != null && primaryGameObject.activeSelf) {
                    primaryGameObject.SetActive(false);
                }
                if (secondaryGameObject != null && secondaryGameObject.activeSelf) {
                    secondaryGameObject.SetActive(false);
                }
                idleGameObject.SetActive(idleGameObjectActiveatable);
            }
            isUpdateNeeded = -1;
        }
    }

    public void OnActionStarted(InputAction.CallbackContext iac) {
        // Debug.LogFormat("[ActionGameObjectSelector] OnActionStarted {0}", iac.action.name);
        activationMap[iac.action] = true;
        isUpdateNeeded = 1;
    }

    public void OnActionPerformed(InputAction.CallbackContext iac) {
        // Debug.LogFormat("[ActionGameObjectSelector] OnActionPerformed {0}", iac.action.name);
    }

    public void OnActionCanceled(InputAction.CallbackContext iac) {
        // Debug.LogFormat("[ActionGameObjectSelector] OnActionCanceled {0}", iac.action.name);
        activationMap[iac.action] = false;
        isUpdateNeeded = 1;
    }

    public void SetGameObjectActiveatable(int i, bool activeatable) {
        if (i == 0) {
            idleGameObjectActiveatable = activeatable;
            isUpdateNeeded = 1;
        }
        else if (i == 1) {
            primaryGameObjectActiveatable = activeatable;
            isUpdateNeeded = 1;
        }
        else if (i == 2) {
            secondaryGameObjectActiveatable = activeatable;
            isUpdateNeeded = 1;
        }
    }
}
