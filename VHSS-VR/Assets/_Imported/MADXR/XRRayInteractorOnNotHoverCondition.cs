using UnityEngine;

public class XRRayInteractorOnNotHoverCondition: BaseMonoBehaviourCondition {
    
    private bool isHovering = false;

    public void setHovering(bool isHovering) {
        Debug.LogFormat("[XRRayInteractorEventStateMonitor] setHovering isHovering={0}", isHovering);
        this.isHovering = isHovering;
    }

    override public bool IsTrue() {
        return !isHovering;
    }
}
