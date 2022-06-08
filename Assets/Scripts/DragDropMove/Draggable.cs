using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Transform originalParent;
    private bool isDrag = false;

    public virtual void Awake()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(transform.parent.GetSiblingIndex());

        InventoryItemData item = null;

        foreach (InventoryItemData data in Inventory.instance.inventory)
            if (data.index == transform.parent.GetSiblingIndex())
                item = data;

        if(item != null)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                isDrag = true;
                //originalParent = transform.parent;
                //transform.SetParent(transform.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            InventoryItemData item = null;

            foreach (InventoryItemData data in Inventory.instance.inventory)
                if (data.index == transform.parent.GetSiblingIndex())
                    item = data;

            if (item != null && eventData.button == PointerEventData.InputButton.Left)
                transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            isDrag = false;
            //transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
