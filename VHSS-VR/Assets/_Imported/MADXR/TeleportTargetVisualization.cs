using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportTargetVisualization : MonoBehaviour {

    private Transform billboard;

    private Transform label;

    [SerializeField]
    private bool showText;

    [SerializeField]
    private bool showTextOnHover;

    [SerializeField]
    private string text;

    private Transform arrow;

    [SerializeField]
    private bool showArrow;

    [SerializeField]
    private float arrowSpeed;

    [SerializeField]
    private float arrowSpan;

    [SerializeField]
    private float visibilityCutoff;

    private Vector3 p0, p;

    private bool hovered;

    public void Start() {

        billboard = transform.GetChild(0);
        arrow = billboard.GetChild(0);
        label = billboard.GetChild(1);

        label.gameObject.SetActive(showText && !showTextOnHover);
        arrow.gameObject.SetActive(showArrow);

        p0 = arrow.localPosition;
        p = p0;

        TeleportationAnchor anchor = GetComponent<TeleportationAnchor>();
        anchor.hoverEntered.AddListener(OnHoverEntered);
        anchor.hoverExited.AddListener(OnHoverExited);

        label.GetChild(0).GetChild(0).GetComponent<Text>().text = text;

        hovered = false;
    }

    public void Update() {

        label.gameObject.SetActive(showText && (!showTextOnHover || hovered));

        if (Vector3.Distance(Camera.main.transform.position, transform.position) < visibilityCutoff) {
            billboard.gameObject.SetActive(false);
        }
        else {
            billboard.gameObject.SetActive(true);
        }

        Quaternion r = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        r.x = 0;
        r.z = 0;
        r.Normalize();
        billboard.localRotation = r;

        if (showArrow && arrowSpeed > 0.0f && arrowSpan > 0.0f) {
            p.Set(p0.x, p0.y + Mathf.Sin(Time.time * arrowSpeed) * arrowSpan, p0.z);
            arrow.localPosition = p;
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs args) {
        hovered = true;
    }

    public void OnHoverExited(HoverExitEventArgs args) {
        hovered = false;
    }
}
