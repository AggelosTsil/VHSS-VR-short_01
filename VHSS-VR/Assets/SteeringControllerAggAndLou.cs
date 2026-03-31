using UnityEngine;

public class SteeringControllerAggAndLou : MonoBehaviour {

    [SerializeField]
    private Transform wheel;
    [SerializeField]
    private Transform body;
    private float lastDegrees;

    [SerializeField]
    [Range(0f, 1f)]
    private float AngleSesitivityY;

    [SerializeField]
    [Range(0f, 1f)]
    private float AngleSesitivityZ;

    private PropulsionController propulsion;

    private int locked;
    public float zVelocity = 0;
    public float smoothTime = 0.2f;

    public void Awake() {
        locked = 0;
    }

    public void Start() {
        propulsion = GetComponent<PropulsionController>();
        if (wheel != null) {
            lastDegrees = wheel.localEulerAngles.z;
        }
    }
    public void Update() {


        if (wheel != null) {

            float degrees = wheel.localEulerAngles.z;
            float diff = degrees - lastDegrees;
            if (diff > 180) diff -= 360;
            if (diff < -180) diff += 360;

            // TODO: needs quaternion math to properly find turn direction since euler
            // angles are always normalized within [0, 360), if at all possible, otherwise
            // consider accumulating wheel rotation deltas per frame and turning depending
            // on total wheel rotation's sign, always checking for crossing over 0/360...

            /*
            if (degrees < 270 && degrees > 180) {
                Vector3 v = wheel.localEulerAngles;
                v.z = 270;
                degrees = 270;
                //this.transform.Rotate(0, AngleSesitivityY, AngleSesitivityZ);
                wheel.localEulerAngles = v;
            }
            else if (degrees < 180 && degrees > 90) {
                Vector3 v = wheel.localEulerAngles;
                v.z = 90;
                degrees = 90;
                //this.transform.Rotate(0, -AngleSesitivityY, -AngleSesitivityZ);
                wheel.localEulerAngles = v;
            }*/

            body.Rotate(new Vector3(0, -AngleSesitivityY * diff, 0));
            body.Rotate(new Vector3(0, 0, -AngleSesitivityZ * diff));
            //propulsion.Turn(-degrees);
            lastDegrees = degrees;

            Vector3 angles = body.transform.localEulerAngles;

            float newZ = Mathf.SmoothDampAngle(
                angles.z,
                0f,
                ref zVelocity,
                smoothTime
            );

            body.transform.localEulerAngles = new Vector3(angles.x, angles.y, newZ);
        }
    }
}
