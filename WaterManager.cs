using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private MeshFilter filter;

    private void Awake()
    {
        filter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = filter.mesh.vertices;
        for(int ii = 0; ii < vertices.Length; ii++)
        {
            float height = transform.position.x + vertices[ii].x;
            vertices[ii].y = WaveGenerator.instance.waveHeight(height);
        }

        filter.mesh.vertices = vertices;
        filter.mesh.RecalculateNormals();
    }
}
