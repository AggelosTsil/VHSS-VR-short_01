using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationAdjust: MonoBehaviour {

    // private IXRSelectInteractor interactor;
    // private IXRSelectInteractable target;

    public void SelectEntered(SelectEnterEventArgs args) {
        // interactor = args.interactorObject;
        // target = args.interactableObject;
        Debug.LogFormat("[TeleportationAdjust] Target selected, {0}...", args.interactableObject.transform.name);
    }

    public void SelectExited(SelectExitEventArgs args) {
        Debug.LogFormat("[TeleportationAdjust] Target deselected, {0}...", args.interactableObject.transform.name);
        // interactor = null;
        // target = null;
    }

    public void HoverExited(HoverExitEventArgs args) {
        // args.interactor.interactionManager.CancelInteractableSelection(args.interactable);
        // args.manager.CancelInteractableSelection(target);
        if (args.interactableObject is IXRSelectInteractable) {
            IXRSelectInteractable s = args.interactableObject as IXRSelectInteractable;
            if (s.isSelected) {
                Debug.LogFormat("[TeleportationAdjust] Deselecting target due to hover exit, {0}...", args.interactableObject.transform.name);
                args.manager.CancelInteractableSelection(s);
            }
        }
    }

    public void HoverEntered(HoverEnterEventArgs args) {
    }
}
