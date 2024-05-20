using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartPageUI : MonoBehaviour
{
    public RawImage Logo;

    private Vector3 initialPosition;
    private float floatingSpeed = 2f;


    void Start()
    {
        // Store initial position of the logo
        initialPosition = Logo.transform.position;
    }

    void Update()
    {
        

        // Scale variation over time
        float scale = 0.8f + (Mathf.Sin(Time.time * floatingSpeed) + 1f) * (0.2f)/2f;
        Logo.transform.localScale = new Vector3(scale, scale, 1f);
    }

}
