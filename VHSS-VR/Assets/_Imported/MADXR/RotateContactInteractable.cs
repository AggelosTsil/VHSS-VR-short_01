using UnityEngine;
using UnityEngine.InputSystem;

public class RotateContactInteractable: ContactInteractable {


    private Vector3 cp, pp, dp;
    private bool left;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance;

    [SerializeField]
    private Quaternion transpose;

    [SerializeField]
    private float sensitivity;

    [SerializeField]
    private bool local;

    [SerializeField]
    private bool lockYaw;

    [SerializeField]
    private bool lockPitch;

    [SerializeField]
    private bool lockRoll;

    //[SerializeField]
    public InputActionReference positionAction;

    [SerializeField]
    //private InputActionReference SecPositionAction; //aggeloukos + Loutsas

    public void SetNewPositionAction(InputActionReference newPositionAction)
    {
        positionAction = newPositionAction;
        cp = positionAction.action.ReadValue<Vector3>();
    }

    public override float GetInteractionDistance() {
        return distance;
    }

    public override InteractionType GetInteractionType() {
        return InteractionType.GRAB;
    }

    public void Start() {
        if (target == null) {
            target = transform;
        }
    }

    public void OnEnable() {

        pp = cp = positionAction.action.ReadValue<Vector3>();
        //pp = cp = SecPositionAction.action.ReadValue<Vector3>(); //aggeloukos +Loutsas
    }
        
    public void Update() {

        pp = cp;

        cp = positionAction.action.ReadValue<Vector3>();
        //cp = SecPositionAction.action.ReadValue<Vector3>(); //aggeloukos + Loutsas

        dp.Set(lockPitch ? 0 : cp.y - pp.y, lockYaw ? 0 : -cp.x + pp.x, lockRoll ? 0 : cp.z - pp.z);

        // Debug.Log("[RotateContactInteractable] Update " + name + ", " + dp * sensitivity);
        // Debug.Log("[RotateContactInteractable] Update " + name + ", " + cp);

        target.Rotate(transpose * dp * sensitivity, local ? Space.Self : Space.World);
    }
}