using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public Rigidbody rb;
    private float displacement = 3f, depth = 1f, floaterCount = 1, waterDrag = 0.99f, angularDrag = 0.5f;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveGenerator.instance.waveHeight(transform.position.x);
        
        if (transform.position.y < waveHeight)
        {
            float clampValue = Mathf.Clamp01((waveHeight - transform.position.y) / depth);
            float multiplier = clampValue * displacement;
            // Debug.Log("=: " + multiplier);
            rb.AddForceAtPosition(new Vector3(0, Mathf.Abs(Physics.gravity.y) * multiplier, 0), transform.position, ForceMode.Acceleration);

            rb.AddForce(multiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(multiplier * -rb.angularVelocity * angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
