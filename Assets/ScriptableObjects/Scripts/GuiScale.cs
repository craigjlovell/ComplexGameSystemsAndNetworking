using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mirror;

public class GuiScale : MonoBehaviour
{
    float parentRectWidth;
    float parentRectHeight;

    public float headerSize;

    public Vector3 velocity;
    
    RectTransform parentRect;
    RectTransform thisRect;

    public float lifetime;


    
    void Start()
    {        
        parentRect = transform.parent.GetComponentInParent<InvSlot>().GetComponent<RectTransform>();
        thisRect = GetComponent<RectTransform>();
        thisRect.anchoredPosition = Vector2.zero;
    }

    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(parentRect.GetComponent<RectTransform>().rect.size.x);

        RpcResise();
    }

    
    
    void RpcResise()
    {
        ReSize();
    }

    
    public void ReSize()
    {
        transform.position += velocity * Time.deltaTime;
        parentRectWidth = parentRect.GetComponent<RectTransform>().rect.size.x;
        parentRectHeight = parentRect.GetComponent<RectTransform>().rect.height;
        thisRect.sizeDelta = new Vector2(parentRectWidth, headerSize);
        
    }


}