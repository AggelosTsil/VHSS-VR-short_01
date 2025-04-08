using UnityEngine;
using UnityEngine.InputSystem;

/**
 * 
 */
public class InteractionController: BaseMonoBehaviourCondition {

    public Transform myHand;
    public InputActionReference positionAction;
    public bool activeInteraction;
    /**
     * 
     */
    [SerializeField]
    private ContactInteractable[] contactInteractables;

    /**
     * 
     */
    [SerializeField]
    private bool disableGameObjectOnDeactivate;

    /**
     * 
     */
    [SerializeField]
    private GameObject pointBehaviour;

    /**
     * 
     */
    [SerializeField]
    private bool pointBehaviourActiveatable;

    /**
     * 
     */
    [SerializeField]
    private GameObject contactBehaviour;

    /**
     * 
     */
    [SerializeField]
    private bool contactBehaviourActiveatable;

    /**
     * 
     */
    [SerializeField]
    private bool activateOnUpdate;

    /**
     * 
     */
    [SerializeField]
    private bool activateOnEnable;

    /**
     * 
     */
    [SerializeField]
    private bool deactivateOnDisable;

    /**
     * 
     */
    [SerializeField]
    private bool deactivateOnRetract;

    /**
     * 
     */
    [SerializeField]
    private InputActionProperty contactAction;

    /**
     * 
     */
    public static ContactInteractable activeContactInteractable = null;

    public override bool IsTrue() {
        return getActivateableContactInteractable() == null;
    }

    /**
     * 
     */
    public ContactInteractable getActivateableContactInteractable() {

        foreach (ContactInteractable contactInteractable in contactInteractables) {

            float d = Vector3.Distance(transform.position, contactInteractable.transform.position);

            if (d < contactInteractable.GetInteractionDistance()) {
                return contactInteractable;
            }
        }

        return null;
    }

    /**
     * 
     */
    protected void Activate() {

        if (activeContactInteractable == null && contactBehaviourActiveatable) {

            // no active contact interactable, see if a new one should be activated...

            ContactInteractable contactInteractable = getActivateableContactInteractable();

            if (contactInteractable != null && activeContactInteractable == null) {

                // contact interactable activated...

                activeContactInteractable = contactInteractable;
                activeInteraction = true;

                Debug.LogFormat("[InteractionController] Activate: Contact interactable activated: {0}", activeContactInteractable.name);

                activeContactInteractable.enabled = true;

                for (int i = 0; i != contactInteractable.transform.childCount; i++) {
                    GameObjectDocker Docker = contactInteractable.transform.GetChild(i).GetComponent<GameObjectDocker>();
                    Docker.interactionController = this;
                    Docker.dockable = myHand;
                    Docker.transform.parent.GetComponent<RotateContactInteractable>().SetNewPositionAction(positionAction);
                    contactInteractable.transform.GetChild(i).gameObject.SetActive(true);
                }

                // activate appropriate child behaviour...
                if (pointBehaviour != null) {
                    pointBehaviour.SetActive(false);
                }
                if (contactBehaviour != null) {
                    contactBehaviour.SetActive(true);
                }
            }

            // choose child behaviour to activate depending on whether a contact interactable was
            // activated or not...
            if (activeContactInteractable == null) {
                if (pointBehaviour != null) {
                    pointBehaviour.SetActive(pointBehaviourActiveatable);
                }
                if (contactBehaviour != null) {
                    contactBehaviour.SetActive(false);
                }
            }
        }
    }

    /**
     * 
     */
    protected void Deactivate() {
        if (activeContactInteractable != null && activeInteraction) {
            activeInteraction = false;
            Debug.LogFormat("[InteractionController] Deactivate: Contact interactable deactivated: {0}", activeContactInteractable.name);
            for (int i = 0; i != activeContactInteractable.transform.childCount; i++) {
                activeContactInteractable.transform.GetChild(i).gameObject.SetActive(false);
            }
            activeContactInteractable.enabled = false;
            activeContactInteractable = null;
        }
        if (pointBehaviour != null) {
            pointBehaviour.SetActive(pointBehaviourActiveatable);
        }
        if (contactBehaviour != null) {
            contactBehaviour.SetActive(false);
        }
        if (disableGameObjectOnDeactivate) {
            gameObject.SetActive(false);
        }
    }

    /**
     * 
     */
    public void Reset() {
        foreach (ContactInteractable contactInteractable in contactInteractables) {
            contactInteractable.enabled = false;
            for (int i = 0; i != contactInteractable.transform.childCount; i++) {
                contactInteractable.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        activeContactInteractable = null;
    }

    /**
     * 
     */
    public void OnEnable() {
        if (activateOnEnable) {
            Activate();
        }
    }

    /**
     * 
     */
    public void OnDisable() {
        if (deactivateOnDisable) {
            Deactivate();
        }
    }

    /**
     * 
     */
    public void Awake() {

        if (contactAction != null) {
            contactAction.action.started += OnActionStarted;
            contactAction.action.performed += OnActionPerformed;
            contactAction.action.canceled += OnActionCanceled;
        }

        if (contactInteractables == null || contactInteractables.Length == 0) {
            contactInteractables = FindObjectsOfType<ContactInteractable>();
        }

        Reset();
    }

    /**
     * 
     */
    public void Start() {
    }

    /**
     * 
     */
    public void Update() {

        if (activeContactInteractable == null) {

            // no active contact interactable...

            if (activateOnUpdate) {
                Activate();
            }
        }

        // bother with deactivation only if deactivating on retract...
        else if (deactivateOnRetract) {

            // there is an active contact interactable, see if it should be deactivated...

            float d = Vector3.Distance(transform.position, activeContactInteractable.transform.position);

            if (d > activeContactInteractable.GetInteractionDistance()) {

                // contact interactable deactivated...

                Deactivate();
            }
        }
    }

    /**
     * 
     */
    public void OnActionStarted(InputAction.CallbackContext iac) {
        // Debug.LogFormat("[InteractionController] OnActionStarted {0}", iac.action.name);
        Activate();
    }

    /**
     * 
     */
    public void OnActionPerformed(InputAction.CallbackContext iac) {
         //Debug.LogFormat("[InteractionController] OnActionPerformed {0}", iac.action.name);
    }

    /**
     * 
     */
    public void OnActionCanceled(InputAction.CallbackContext iac) {
        // Debug.LogFormat("[InteractionController] OnActionCanceled {0}", iac.action.name);
        Deactivate();
    }

    /**
     * 
     */
    public ContactInteractable GetActiveInteractable() {
        return activeContactInteractable;
    }

    /**
     * 
     */
    public void SetPointBehaviourActiveatable(bool activeatable) {
        pointBehaviourActiveatable = activeatable;
        Activate();
    }

    /**
     * 
     */
    public void SetContactBehaviourActiveatable(bool activeatable) {
        contactBehaviourActiveatable = activeatable;
        Activate();
    }
}
