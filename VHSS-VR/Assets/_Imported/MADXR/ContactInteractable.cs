using UnityEngine;

public abstract class ContactInteractable: MonoBehaviour {

    public enum InteractionType {
        GENERIC,
        GRAB,
        PRESS
    };

    public abstract InteractionType GetInteractionType();

    public abstract float GetInteractionDistance();

    public virtual void OnDrawGizmos() {
        Color c = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetInteractionDistance());
        Gizmos.color = c;
    }
}
