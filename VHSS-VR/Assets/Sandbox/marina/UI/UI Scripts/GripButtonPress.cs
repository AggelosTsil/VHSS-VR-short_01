using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
[System.Serializable]
public class GripButtonEvent : UnityEvent<bool> { }

public class GripButtonPress : MonoBehaviour
{
    public GripButtonEvent gripButtonPress;
    private bool lastButtonState = false;
    private List<UnityEngine.XR.InputDevice> allDevices;
    private List<UnityEngine.XR.InputDevice> devicesWithGripButton;

    void Start()
    {
        if (gripButtonPress == null)
        {
            gripButtonPress = new GripButtonEvent();
        }

        allDevices = new List<UnityEngine.XR.InputDevice>();
        devicesWithGripButton = new List<UnityEngine.XR.InputDevice>();
        InputTracking.nodeAdded += InputTracking_nodeAdded;
    }

    // check for new input devices when new XRNode is added
    private void InputTracking_nodeAdded(XRNodeState obj)
    {
        updateInputDevices();
    }

    void Update()
    {
        bool tempState = false;
        bool invalidDeviceFound = false;
        foreach (var device in devicesWithGripButton)
        {
            bool buttonState = false;
            tempState = device.isValid // the device is still valid
                        && device.TryGetFeatureValue(CommonUsages.gripButton, out buttonState) // did get a value
                        && buttonState // the value we got
                        || tempState; // cumulative result from other controllers

            if (!device.isValid)
                invalidDeviceFound = true;
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            gripButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }

        if (invalidDeviceFound || devicesWithGripButton.Count == 0) // refresh device lists
            updateInputDevices();
    }

    // find any devices supporting the desired feature usage
    void updateInputDevices()
    {
        devicesWithGripButton.Clear();
        UnityEngine.XR.InputDevices.GetDevices(allDevices);
        bool discardedValue;

        foreach (var device in allDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out discardedValue))
            {
                devicesWithGripButton.Add(device); // Add any devices that have a primary button.
            }
        }
    }
}
