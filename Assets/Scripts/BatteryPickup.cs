using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 60f;
    [SerializeField] float intensityAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(intensityAmount);
            Destroy(gameObject);
        }
    }

}
