using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform originalParent;
    public InventoryItemData itemData;

    public bool isDrag;

    public virtual void Awake()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Inventory.instance.inventory[transform.parent.GetSiblingIndex()] != null)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                isDrag = true;
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Inventory.instance.inventory[transform.parent.GetSiblingIndex()] != null && eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            isDrag = false;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
