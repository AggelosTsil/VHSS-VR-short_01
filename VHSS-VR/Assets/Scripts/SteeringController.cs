using UnityEngine;

public class SteeringController : MonoBehaviour {

    [SerializeField]
    private Transform wheel;

    private PropulsionController propulsion;

    private int locked;

    public void Awake() {
        locked = 0;
    }

    public void Start() {
        propulsion = GetComponent<PropulsionController>();
    }

    public void Update() {


        if (wheel != null) {

            float degrees = wheel.localEulerAngles.z;

            // TODO: needs quaternion math to properly find turn direction since euler
            // angles are always normalized within [0, 360), if at all possible, otherwise
            // consider accumulating wheel rotation deltas per frame and turning depending
            // on total wheel rotation's sign, always checking for crossing over 0/360...

            if (degrees < 270 && degrees > 180) {
                Vector3 v = wheel.localEulerAngles;
                v.z = 270;
                degrees = 270;
                wheel.localEulerAngles = v;
            }
            else if (degrees < 180 && degrees > 90) {
                Vector3 v = wheel.localEulerAngles;
                v.z = 90;
                degrees = 90;
                wheel.localEulerAngles = v;
            }

            propulsion.Turn(-degrees);
        }
    }
}
