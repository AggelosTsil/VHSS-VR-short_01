using UnityEngine;

public class DebugTool : MonoBehaviour {

    public void Start() {
    }

    public void Update() {

    }

    public void DebugMessage(string text) {
        Debug.Log(text);
    }

    protected void OnDisable() {
    }
}
