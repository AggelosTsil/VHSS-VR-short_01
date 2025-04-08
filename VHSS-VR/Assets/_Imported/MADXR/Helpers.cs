using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Helpers: MonoBehaviour {

    [SerializeField]
    private Transform throwCube;

    private Vector3 throwCubePosition;
    private Quaternion throwCubeRotation;

    [SerializeField]
    private InputActionProperty throwCubeResetAction;

    private Rigidbody rigidbodyThrowCube;

    [SerializeField]
    private InputActionProperty gameStateSwitchAction;

    public void Start() {

        Debug.Log("Saving throw cube transform...");
        throwCubePosition = throwCube.position;
        throwCubeRotation = throwCube.rotation;

        Debug.Log("Enabling game state switch action...");
        gameStateSwitchAction.action.Enable();

        Debug.Log("Enabling throw cube reset action...");
        throwCubeResetAction.action.Enable();

        rigidbodyThrowCube = throwCube.GetComponent<Rigidbody>();

        throwCubeResetAction.action.canceled += ThrowCubeReset;
    }

    public void Update() {

        if (gameStateSwitchAction.action.WasPerformedThisFrame()) {
            if (GameManager.GetInstance().GetGameState() == GameManager.GameState.EXPLORING) {
                GameManager.GetInstance().SetGameState(GameManager.GameState.INSPECTING);
            }
            else {
                GameManager.GetInstance().SetGameState(GameManager.GameState.EXPLORING);
            }
        }
    }

    public void ThrowCubeReset(InputAction.CallbackContext ctx) {
        Debug.Log("Resetting throw cube...");
        rigidbodyThrowCube.velocity = Vector3.zero;
        rigidbodyThrowCube.angularVelocity = Vector3.zero;
        throwCube.transform.position = throwCubePosition;
        throwCube.transform.rotation = throwCubeRotation;
    }
}
