using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    string lhcTAG = "Left hand controller";
    string rhcTAG = "Right hand controller";
    private InputDevice leftHandController;
    private InputDevice rightHandController;

    void Start()
    {
        // List available devices on Unity
        List<InputDevice> listDevices = new List<InputDevice>();
        InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        // Populate listDevices
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, listDevices);

        foreach (var device in listDevices)
            Debug.Log("Device characteristics: " + device.characteristics);

        // Find proper controllers
        if (listDevices.Count > 0){
            foreach (var device in listDevices)
            {
                if (device.characteristics == InputDeviceCharacteristics.Left)
                    leftHandController = device;
                if (device.characteristics == InputDeviceCharacteristics.Right)
                    rightHandController = device;
            }
            Debug.Log("Successfully found devices.")
        }
        else Debug.Log("No valid devices found");
    }

    // Called every frame 
    void Update()
    {
        // Check for button inputs 
        if (leftHandController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log(lhcTAG + "Primary button pressed");

        if (leftHandController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log(rhcTAG + "Primary button pressed");
    }

}