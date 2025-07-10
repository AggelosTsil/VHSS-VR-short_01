using UnityEngine;
using UnityEngine.InputSystem;

public class Climbing : ContactInteractable
{


    private Vector3 cp, pp, dp;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance;

    [SerializeField]
    private Quaternion transpose;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private bool local;

    [SerializeField]
    private bool lockYaw;

    [SerializeField]
    private bool lockPitch;

    [SerializeField]
    private bool lockRoll;

    //[SerializeField]
    public InputActionReference positionAction;//left hand 

    public InputActionReference SecPositionAction;//for both hands to work this is tied to the right hand position and called when the right hand is grabbing

    public InputAction Grabbing;
    public void SetNewPositionAction(InputActionReference newPositionAction)
    {
        positionAction = newPositionAction;
        cp = positionAction.action.ReadValue<Vector3>();
    }

    public override float GetInteractionDistance()
    {
        return distance;
    }

   public override InteractionType GetInteractionType()
    {
        return InteractionType.GRAB;
    }

    public void Start()
    {
        if (target == null)
        {
            target = transform;
        }
        Grabbing.Enable();
    }

    public void OnEnable()
    {

        pp = cp = positionAction.action.ReadValue<Vector3>();
        
    }

    public void Update()
    {
        if (Grabbing.triggered)
        {
            pp = cp;

            cp = positionAction.action.ReadValue<Vector3>();


            //dp.Set(lockPitch ? 0 : cp.y - pp.y, lockYaw ? 0 : -cp.x + pp.x, lockRoll ? 0 : cp.z - pp.z);
            dp.Set(cp.z - pp.z, -cp.y + pp.y, -cp.x + pp.x);
            // Debug.Log("[RotateContactInteractable] Update " + name + ", " + dp * sensitivity);
            // Debug.Log("[RotateContactInteractable] Update " + name + ", " + cp);

            target.Translate(dp * speed, local ? Space.Self : Space.World);
        }
    }
}