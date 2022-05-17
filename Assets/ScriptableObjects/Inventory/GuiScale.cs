using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mirror;

public class GuiScale : NetworkBehaviour
{
    float parentRectWidth;
    float parentRectHeight;

    public float headerSize;

    public Vector3 velocity;
    
    RectTransform parentRect;
    RectTransform thisRect;

    public float lifetime;

    [Client]
    public void Awake()
    {
        thisRect.anchoredPosition = Vector2.zero;
    }

    [Client]
    void Start()
    {        
        parentRect = transform.parent.GetComponentInParent<InvSlot>().GetComponent<RectTransform>();
        thisRect = GetComponent<RectTransform>();        
    }

    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(parentRect.GetComponent<RectTransform>().rect.size.x);

        RpcResise();
    }

    
    [Client]
    void RpcResise()
    {
        ReSize();
    }

    [Client]
    public void ReSize()
    {
        transform.position += velocity * Time.deltaTime;
        parentRectWidth = parentRect.GetComponent<RectTransform>().rect.size.x;
        parentRectHeight = parentRect.GetComponent<RectTransform>().rect.height;
        thisRect.sizeDelta = new Vector2(parentRectWidth, headerSize);
        
    }


}
