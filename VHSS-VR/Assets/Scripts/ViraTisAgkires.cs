using UnityEngine;

public class ViraTisAgkires : MonoBehaviour {

    private static string PARAM_SPEED = "speed";

    [SerializeField]
    private Animator chainAnimator;

    [SerializeField]
    private Material ropeMaterial;

    [SerializeField]
    private float ropeSpeedAdjust;

    [SerializeField]
    private float chainSpeedAdjust;

    // private Quaternion oldRotation;
    private float oldRotation;

    private int paramSpeed = Animator.StringToHash(PARAM_SPEED);

    protected void setChainSpeed(float speed) {
        chainAnimator.SetFloat(paramSpeed, speed);
    }

    protected void moveRope(float ds) {
        ropeMaterial.mainTextureOffset += new Vector2(0, ds * ropeSpeedAdjust);
    }

    public void Start() {
        oldRotation = transform.localRotation.eulerAngles.z;
        setChainSpeed(0);
    }

    public void Update() {
        // float dr = Quaternion.Angle(oldRotation, transform.rotation);
        float dr = -(transform.localRotation.eulerAngles.z - oldRotation);
        oldRotation = transform.localRotation.eulerAngles.z;
        if (Mathf.Abs(dr) > 0.01f) {
            if (dr > 0.01f) {
                setChainSpeed(dr * chainSpeedAdjust /* / anim.GetCurrentAnimatorStateInfo(0).speed */);
            }
            else {
                setChainSpeed(0);
            }
            moveRope(dr);
        }
    }
}
