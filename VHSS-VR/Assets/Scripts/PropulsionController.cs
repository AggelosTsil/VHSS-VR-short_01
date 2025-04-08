using UnityEngine;

public class PropulsionController: MonoBehaviour {

    private Rigidbody body;

    private Vector3 force;
    private Vector3 torque;

    [SerializeField]
    private Vector3 pointOfApplication;

    [SerializeField]
    private Quaternion orientation;

    [SerializeField]
    private float speed;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float turn;

    [SerializeField]
    private float turnDrag;

    public Vector3 GetForce() {
        return force;
    }

    public Vector3 GetTorque() {
        return torque;
    }

    public void Turn(float degrees) {
        orientation = Quaternion.AngleAxis(degrees, Vector3.up);
    }

    public void Start() {
    }

    public void Update() {

    }

    public void FixedUpdate() {

        if (body == null) {
            body = GetComponent<Rigidbody>();
        }

        if (turn > 1) {
            turn = 1;
        }
        else if (turn < 0) {
            turn = 0;
        }

        force = transform.rotation * Vector3.forward * speed;

        torque = transform.rotation * orientation * Vector3.forward * turn * speed / turnDrag;

        Vector3 worldPointOfApplication = transform.position + transform.rotation * pointOfApplication;

        Debug.DrawLine(transform.position, transform.position + force, Color.red);
        Debug.DrawLine(worldPointOfApplication, worldPointOfApplication + torque, Color.red);
        // Debug.DrawLine(transform.position - transform.rotation * pointOfApplication, transform.position - transform.rotation * pointOfApplication - torque, Color.red);

        if (body != null) {
            body.AddForce(force, ForceMode.Acceleration);
            body.AddForceAtPosition(torque, worldPointOfApplication, ForceMode.Acceleration);
            // body.AddForceAtPosition(-torque, -transform.position + transform.rotation * pointOfApplication, ForceMode.Acceleration);
        }
    }

    public void OnGUI() {
        
        GUI.Label(new Rect(10, 10, 200, 30), "Force: " + force.ToString());
        GUI.Label(new Rect(10, 50, 200, 30), "Torque: " + torque.ToString());
    }
}
