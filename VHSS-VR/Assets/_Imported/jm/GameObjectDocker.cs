using UnityEngine;

public class GameObjectDocker : MonoBehaviour {

    //[SerializeField]
    public Transform dockable;

    [SerializeField]
    private Animator animator;

    //[SerializeField]
    public InteractionController interactionController;

    [SerializeField]
    private bool dockOnEnableDisable;

    [SerializeField]
    private bool dockOnUpdate;

    [SerializeField]
    private Vector3 adjustPosition;
    [SerializeField]
    private Vector3 adjustPositionLeft;

    // [SerializeField]
    // private Quaternion adjustRotation;

    private Transform parent;

    private Follower follower;

    private Vector3 position;
    private Quaternion rotation;

    public void Dock() {

        if (interactionController.GetActiveInteractable() != null) {

            position = dockable.localPosition;
            rotation = dockable.localRotation;

            // Transform target = interactionController.GetActiveInteractable().transform;
            Transform target = transform;

            parent = dockable.parent;

            Debug.LogFormat("[GameObjectDocker] Dock, dockable: {0}, target: {1}, parent: {2}", dockable, interactionController.GetActiveInteractable(), parent);

            dockable.parent = null;
            follower = dockable.gameObject.AddComponent<Follower>();
            follower.target = target;
            if (dockable.gameObject.name.StartsWith("Right"))  //THA TO ALLAXO
                follower.translationOffset = adjustPosition;
            else follower.translationOffset = adjustPositionLeft;
            // follower.orientationOffset = adjustRotation;

            dockable.localPosition = Vector3.zero;
            // dockable.localRotation = adjustRotation;
        }
    }

    public void Undock() {

        if (parent != null) {

            Debug.LogFormat("[GameObjectDocker] Undock, dockable: {0}, target: {1}, parent: {2}", dockable, dockable.parent, parent);

            Destroy(follower);

            dockable.parent = parent;
            parent = null;

            dockable.localPosition = Vector3.zero;
            dockable.localRotation = Quaternion.identity;
            //dockable.localPosition = position;
            //dockable.localRotation = rotation;
        }
    }

    public void OnEnable() {
        if (dockOnEnableDisable) {
            // Debug.LogFormat("[GameObjectDocker] OnEnable, target: {0}", interactionController.GetActiveInteractable());
            Dock();
        }
    }

    public void OnDisable() {
        if (dockOnEnableDisable) {
            // Debug.LogFormat("[GameObjectDocker] OnDisable, target: {0}", interactionController.GetActiveInteractable());
            Undock();
        }
    }

    public void Update() {
        if (dockOnUpdate) {
            // Debug.LogFormat("[GameObjectDocker] OnUpdate, target: {0}", interactionController.GetActiveInteractable());
        }
    }
}
