using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StringRenderer : MonoBehaviour
{
    public float stringLength = 50.0f; // Length of the string

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            // Set the start and end points of the string
            Vector3 startPoint = transform.position; // Image position (start of the string)
            Vector3 endPoint = startPoint + Vector3.up * stringLength; // End point directly above

            // Assign the positions to the LineRenderer
            lineRenderer.SetPosition(0, startPoint); // Start point at image
            lineRenderer.SetPosition(1, endPoint);   // End point upwards
        }
    }
}

