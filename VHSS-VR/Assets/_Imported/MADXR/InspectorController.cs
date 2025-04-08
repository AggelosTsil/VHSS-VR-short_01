using UnityEngine;
using UnityEngine.InputSystem;

public class InspectorController : MonoBehaviour {

    [SerializeField]
    private InputActionProperty yawAction;

    [SerializeField]
    private InputActionProperty pitchAction;

    [SerializeField]
    private Transform inspectionTarget;

    [SerializeField]
    private Vector3 initialOffset;

    [SerializeField]
    private Vector3 initialOrientation;

    public void Start() {
    }

    public void Update() {
        transform.position = inspectionTarget.position;
    }

    public void OnEnable() {
        yawAction.action.performed += OnYawActionPerformed;
        pitchAction.action.performed += OnPitchActionPerformed;
    }

    public void OnDisable() {
        yawAction.action.performed -= OnYawActionPerformed;
        pitchAction.action.performed -= OnPitchActionPerformed;
    }

    public Transform GetInspectionTarget() {
        return inspectionTarget;
    }

    public Vector3 GetInitialOffset() {
        return new Vector3(initialOffset.x, initialOffset.y, initialOffset.z);
    }

    public Quaternion GetInitialOrientation() {
        return Quaternion.Euler(initialOrientation);
    }

    protected void OnYawActionPerformed(InputAction.CallbackContext ctx) {
        float a = ctx.action.ReadValue<Vector2>().x * 90 * Time.deltaTime;
        Vector3 r = transform.localRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(r.x, r.y + a, r.z);
    }
    
    protected void OnPitchActionPerformed(InputAction.CallbackContext ctx) {
        float a = ctx.action.ReadValue<Vector2>().y * 90 * Time.deltaTime;
        Vector3 r = transform.localRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(r.x + a, r.y, r.z);
    }
}
