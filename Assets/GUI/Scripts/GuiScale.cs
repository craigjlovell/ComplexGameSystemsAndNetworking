using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mirror;

public class GuiScale : NetworkBehaviour
{
    float parentRectWidth = 0.0f;
    float parentRectHeight = 0.0f;

    public float headerSize;

    RectTransform parentRect;

    public Vector3 velocity;

    RectTransform thisRect;
    void Start()
    {
        parentRect = transform.parent.GetComponentInChildren<InvSlot>().GetComponent<RectTransform>();
        thisRect = GetComponent<RectTransform>();
    }
    
    // Update is called once per frame
    void Update()
    {


        transform.position += velocity * Time.deltaTime;
        Debug.Log(transform.parent.GetComponent<RectTransform>().rect.width);
        parentRectWidth = parentRect.rect.width;
        parentRectHeight = parentRect.rect.height;
        thisRect.sizeDelta = new Vector2(parentRectWidth, headerSize);
        thisRect.position = new Vector2(parentRect.position.x, parentRectHeight);
    }
}
