using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCode : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform origin;
    public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 3;
        lineRenderer.endWidth = 3;

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.localPosition);
        lineRenderer.SetPosition(1, destination.localPosition);
    }
}
