using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ClickInteractable: XRBaseInteractable {

    [SerializeField]
    private bool clickOnDeactivate;

    [SerializeField]
    private UnityEvent triggerEvents;

    private ActionBasedController xrController;
    private Transform target;

    protected override void OnActivated(ActivateEventArgs args) {
        base.OnActivated(args);
        // Debug.LogFormat("[{0}] {1}", "InspectionInteractable", "OnActivated");
        xrController = args.interactorObject.transform.GetComponent<ActionBasedController>();
        if (xrController == null) {
            Debug.LogFormat("[{0}] {1}", "ClickInteractable", "WARNING: No XRController found on interactor!");
        }
        else {
            target = args.interactableObject.transform;
            StartInteraction();
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args) {
        base.OnDeactivated(args);
        // Debug.LogFormat("[{0}] {1}", "InspectionInteractable", "OnDeactivated");
        StopInteraction();
    }

    protected virtual void StartInteraction() {
        Debug.LogFormat("[{0}] {1}", "InspectionInteractable", "Interacting with " + target.name +
            " using " + xrController.name);
        if (!clickOnDeactivate) {
            Click();
        }
    }

    protected virtual void StopInteraction() {
        Debug.LogFormat("[{0}] {1}", "InspectionInteractable", "Interaction ended...");
        xrController = null;
        target = null;
        if (clickOnDeactivate) {
            Click();
        }
    }

    public void Update() {

        if (xrController != null) {

            if (xrController.activateAction.action.WasReleasedThisFrame()) {
                StopInteraction();
            }
        }
    }

    protected void Click() {
        Debug.LogFormat("[ClickInteractable] Clicked, {0}...", name);
        triggerEvents.Invoke();
    }
}