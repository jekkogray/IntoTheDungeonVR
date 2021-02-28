using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    public InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    public bool showController;
    private Animator handAnimator;


    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        // List available devices on Unity
        List<InputDevice> deviceList = new List<InputDevice>();

        // Populate listDevices
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, deviceList);

        foreach (var device in deviceList)
            Debug.Log(device.name + device.characteristics);
        // Get the input device
        if (deviceList.Count > 0)
        {
            targetDevice = deviceList[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
                spawnedController = Instantiate(prefab, transform);
            else
            {
                Debug.Log("Did not find corresponding controller model.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }

    }


    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            handAnimator.SetFloat("Trigger", triggerValue);
        else
            handAnimator.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            handAnimator.SetFloat("Grip", gripValue);
        else
            handAnimator.SetFloat("Grip", 0);
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}