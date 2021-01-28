using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] RigidbodyFirstPersonController fpController;
    float zoomedInFOV = 30f;
    float zoomedOUTFOV = 60f;
    bool isZoomed = false;

    [SerializeField] float zoomedSens = .5f;
    [SerializeField] float outZoomedSens = 2f;
    private void OnDisable()
    {
        ZoomOut();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleZoom();
        }
    }
    public void ToggleZoom()
    {
        if (!isZoomed)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }
    public void ZoomOut()
    {
        isZoomed = false;
        fpController.mouseLook.XSensitivity = outZoomedSens;
        fpController.mouseLook.YSensitivity = outZoomedSens;
        camera.fieldOfView = zoomedOUTFOV;
    }

    private void ZoomIn()
    {
        isZoomed = true;
        fpController.mouseLook.XSensitivity = zoomedSens;
        fpController.mouseLook.YSensitivity = zoomedSens;
        camera.fieldOfView = zoomedInFOV;
    }
}
