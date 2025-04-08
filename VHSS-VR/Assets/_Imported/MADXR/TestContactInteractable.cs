using UnityEngine;

public class TestContactInteractable: ContactInteractable {

    public static float DEFAULT_INTERACTION_DISTANCE = 0.5f;

    public override float GetInteractionDistance() {
        return DEFAULT_INTERACTION_DISTANCE;
    }

    public override InteractionType GetInteractionType() {
        return InteractionType.GRAB;
    }

    public void Update() {
    }

    public void OnEnable() {
        Debug.LogFormat(this, "I am {0} and I am now active!", name);
    }

    public void OnDisable() {
        Debug.LogFormat(this, "I am {0} and I am now not active!", name);
    }
}