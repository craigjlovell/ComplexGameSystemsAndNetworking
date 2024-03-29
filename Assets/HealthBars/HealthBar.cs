﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{

    public Health health;
    public Image image;

    // Use this for initialization
    void Start ()
    {
        if (image)
        {
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Horizontal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = health.transform.position + Vector3.up * 2;
        transform.forward = Camera.main.transform.forward;
        UpdateMeter();
    }

    public void UpdateMeter()
    { 
        // scale the meter
        float pct = Mathf.Clamp01(health.health / health.maxHealth);
        image.fillAmount = pct;
    }
}
