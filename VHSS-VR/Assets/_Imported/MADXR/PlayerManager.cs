using UnityEngine;

public class PlayerManager: MonoBehaviour {

    [SerializeField]
    private Transform cameraOffset;

    [SerializeField]
    private bool useExplorationCharacterController;

    [SerializeField]
    private CharacterController explorationCharacterController;

    [SerializeField]
    private bool useInspectionCharacterController;

    [SerializeField]
    private InspectorController inspectionCharacterController;

    [SerializeField]
    private GameObject[] explorationXRControllerBehaviours;

    [SerializeField]
    private GameObject[] inspectionXRControllerBehaviours;

    private class MADTransform {
        
        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale;

        public MADTransform() {
            position = Vector3.zero;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }

        public MADTransform(Vector3 p, Quaternion r, Vector3 s) {
            position = p;
            rotation = r;
            scale = s;
        }

        public MADTransform(Transform t) {
            position = t.localPosition;
            rotation = t.localRotation;
            scale = t.localScale;
        }

        public void CopyFrom(Transform t) {
            position = t.localPosition;
            rotation = t.localRotation;
            scale = t.localScale;
        }

        public void CopyTo(Transform t) {
            t.localPosition = position;
            t.localRotation = rotation;
            t.localScale = scale;
        }
    }

    private MADTransform lastExplorerOriginTransform;
    private MADTransform lastExplorerCameraOffsetTransform;

    private Quaternion lastInspectorOriginOrientation;
    private Vector3 lastInspectorCameraOffsetPosition;

    public void Start() {

        explorationCharacterController.enabled = useExplorationCharacterController;
        inspectionCharacterController.enabled = useInspectionCharacterController;

        // exploration starts from wherever the player has been originally put in the world...
        lastExplorerOriginTransform = new MADTransform(transform);
        lastExplorerCameraOffsetTransform = new MADTransform(cameraOffset);

        // inspection starts from a default position...
        lastInspectorOriginOrientation = inspectionCharacterController.GetInitialOrientation();
        lastInspectorCameraOffsetPosition = inspectionCharacterController.GetInitialOffset();

        Explore();
    }

    public void Update() {

    }

    public void SaveExplorationTransforms() {
        lastExplorerOriginTransform.CopyFrom(transform);
        lastExplorerCameraOffsetTransform.CopyFrom(cameraOffset);
    }

    public void SaveInspectionTransforms() {
        lastInspectorOriginOrientation = transform.localRotation;
        lastInspectorCameraOffsetPosition = cameraOffset.localPosition;
    }

    public void Explore() {

        lastExplorerOriginTransform.CopyTo(transform);
        lastExplorerCameraOffsetTransform.CopyTo(cameraOffset);

        if (useInspectionCharacterController) {
            inspectionCharacterController.enabled = false;
        }
        foreach (GameObject inspectionXRControllerBehaviour in inspectionXRControllerBehaviours) {
            inspectionXRControllerBehaviour.SetActive(false);
        }

        if (useExplorationCharacterController) {
            explorationCharacterController.enabled = true;
        }
        foreach (GameObject explorationXRControllerBehaviour in explorationXRControllerBehaviours) {
            explorationXRControllerBehaviour.SetActive(true);
        }
    }

    public void Inspect() {
        transform.localPosition = inspectionCharacterController.GetInspectionTarget().position;
        transform.localRotation = lastInspectorOriginOrientation;
        transform.localScale = Vector3.one;
        cameraOffset.localPosition = lastInspectorCameraOffsetPosition;
        cameraOffset.localRotation = Quaternion.identity;
        cameraOffset.localScale = Vector3.one;

        if (useExplorationCharacterController) {
            explorationCharacterController.enabled = false;
        }
        foreach (GameObject explorationXRControllerBehaviour in explorationXRControllerBehaviours) {
            explorationXRControllerBehaviour.SetActive(false);
        }

        if (useInspectionCharacterController) {
            inspectionCharacterController.enabled = true;
        }
        foreach (GameObject inspectionXRControllerBehaviour in inspectionXRControllerBehaviours) {
            inspectionXRControllerBehaviour.SetActive(true);
        }
    }
}
