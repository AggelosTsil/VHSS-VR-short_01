using UnityEngine;

public class Follower: MonoBehaviour {

    [SerializeField]
    public Transform target;

    [SerializeField]
    public Vector3 translationOffset;

    // [SerializeField]
    // public Quaternion orientationOffset;

    public void LateUpdate() {
        // transform.rotation = target.rotation * orientationOffset;
        transform.rotation = target.rotation;
        transform.position = target.position + transform.rotation * translationOffset;
    }
}
