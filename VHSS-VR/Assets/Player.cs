using UnityEngine;

/**
 * An application-specific helper class for centralized control of the player (i.e., the user's
 * avatar and its components including hands and other effectors).
 */
public class Player: MonoBehaviour {

    public enum State {
        NO_INTERACTION,
        EXPLORE,
        NAVIGATE,
        INTERACT
    }

    [SerializeField]
    private ActionGameObjectSelector navigationController;

    [SerializeField]
    private InteractionController interactionController;
    [SerializeField]
    private InteractionController lefthandInteractionController;

    [SerializeField]
    private State state;

    [SerializeField]
    private State initialState;

    private static Player inst = null;

    public static Player GetPlayer() {
        return inst;
    }

    public void Awake() {
        inst = this;
    }

    public void Start() {
        SetState(initialState);
    }

    public void SetState(State state) {

        switch (state) {

            case State.NO_INTERACTION:
                navigationController.SetGameObjectActiveatable(0, false);
                navigationController.SetGameObjectActiveatable(1, false);
                interactionController.SetPointBehaviourActiveatable(false);
                interactionController.SetContactBehaviourActiveatable(true);
                lefthandInteractionController.SetPointBehaviourActiveatable(false);
                lefthandInteractionController.SetContactBehaviourActiveatable(true);
                break;

            case State.EXPLORE:
                navigationController.SetGameObjectActiveatable(0, true);
                navigationController.SetGameObjectActiveatable(1, false);
                interactionController.SetPointBehaviourActiveatable(true);
                interactionController.SetContactBehaviourActiveatable(false);
                lefthandInteractionController.SetPointBehaviourActiveatable(true);
                lefthandInteractionController.SetContactBehaviourActiveatable(false);
                break;

            case State.NAVIGATE:
                navigationController.SetGameObjectActiveatable(0, false);
                navigationController.SetGameObjectActiveatable(1, true);
                interactionController.SetPointBehaviourActiveatable(false);
                interactionController.SetContactBehaviourActiveatable(false);
                lefthandInteractionController.SetPointBehaviourActiveatable(false);
                lefthandInteractionController.SetContactBehaviourActiveatable(false);

                break;

            case State.INTERACT:
                navigationController.SetGameObjectActiveatable(0, true);
                navigationController.SetGameObjectActiveatable(1, false);
                interactionController.SetPointBehaviourActiveatable(false);
                interactionController.SetContactBehaviourActiveatable(true);
                lefthandInteractionController.SetPointBehaviourActiveatable(false);
                lefthandInteractionController.SetContactBehaviourActiveatable(true);
                break;
        }

        Debug.Log("[Player] SetState: state=" + this.state);
    }
}

