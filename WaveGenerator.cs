using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public static WaveGenerator instance;

    private float
        speed = 1f,
        length = 2f,
        offset = 0f, 
        amplitude = 1f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public float waveHeight(float height) => amplitude * Mathf.Sin(height / length + offset);

    private void FixedUpdate()
    {
        offset += Time.deltaTime * speed;
    }
}
