using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GuiScale : MonoBehaviour
{
    float parentRect = 0.0f;

    public float headerSize;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.parent.GetComponent<RectTransform>().rect.width);
        parentRect = transform.parent.GetComponent<RectTransform>().rect.width;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(parentRect, headerSize);
    }
}
