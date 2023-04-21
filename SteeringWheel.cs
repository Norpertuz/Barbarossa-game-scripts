using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public bool detected { get { return active; } }
    private bool active;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) active = true;
        else active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) active = true;
        else active = false;
    }
}
