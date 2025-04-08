using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RotationGoal: MonoBehaviour {

    public enum Axis {
        X, Y, Z
    }

    [SerializeField]
    private Axis useAxis;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private float goal;

    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private UnityEvent onGoalAchieved;

    [SerializeField]
    private UnityEvent onFirstSuccess;

    [SerializeField]
    private UnityEvent onTimeLimit;

    [SerializeField]
    private float changeThreshold = 1;

    [Header("Current values (for debugging)")]

    [SerializeField]
    private float achieved;

    [SerializeField]
    private float totalTime;

    [SerializeField]
    private bool finished;

    private float oldRotation;

    private bool changing = false;

    private AudioSource aud;

    public bool firstSuccess = false;

    public void Awake() {
        aud = GetComponent<AudioSource>();
    }

    public void Start() {
        if (goal < 0) {
            throw new ArgumentException();
        }
        achieved = 0;
        totalTime = 0;
        oldRotation = GetCurrentAngle();
        finished = false;
    }

    public void Update() {

        if (finished) {
            return;
        }

        if (timeLimit > 0) {
            totalTime += Time.deltaTime;
            if (totalTime > timeLimit) {
                Debug.Log("[RotationGoal] Update: Time limit reached");
                if (aud != null) {
                    //aud.Stop();
                    StartCoroutine(FadeOut(aud, 0.5f));
                }
                finished = true;
                onTimeLimit.Invoke();
                return;
            }
        }

        if (achieved > goal) {
            Debug.Log("[RotationGoal] Update: Goal achieved");
            if (aud != null) {
                //aud.Stop();
                StartCoroutine(FadeOut(aud, 0.5f));
            }
            finished = true;
            onGoalAchieved.Invoke();
            return;
        }

        float cr = GetCurrentAngle();
        float dr = (cr - oldRotation) * (invert ? -1 : 1);
        if (dr > 180.0f) {
            dr = 0;
        }
        oldRotation = cr;

        if (Mathf.Abs(dr) > 0.01f) {
            if (achieved + dr > achieved) {
                achieved += dr;
            }
        }

        if (changeThreshold > 0.001f) {

            if (!changing && Mathf.Abs(dr) > changeThreshold) {
                changing = true;
                Debug.Log("[RotationGoal] Update: Started changing");
                if(!firstSuccess) {
                    firstSuccess = true;
                    onFirstSuccess.Invoke();
                }
                // handle started changing...
                if (aud != null) {
                    aud.Play();
                }
            }
            else if (changing && Mathf.Abs(dr) < changeThreshold) {
                changing = false;
                Debug.Log("[RotationGoal] Update: Stopped changing");
                // handle stopped changing...
                if (aud != null) {
                    //aud.Stop();
                    StartCoroutine(FadeOut(aud, 0.5f));
                }
            }
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    protected float GetCurrentAngle() {
        float r = 0;
        switch (useAxis) {
            case Axis.X:
                r = transform.localEulerAngles.x;
                break;
            case Axis.Y:
                r = transform.localEulerAngles.y;
                break;
            case Axis.Z:
                r = transform.localEulerAngles.z;
                break;
        }

        return r;
    }
}