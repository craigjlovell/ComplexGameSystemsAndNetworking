using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class InventorySlot : Inventory, IDropHandler//, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject slotGameobject;

    public void UpdateSlot(InventoryItemData data)
    {
        if (data != null)
        {
            slotGameobject.GetComponent<Image>().sprite = data.itemPrefab.GetComponent<InvImage>().itemImage;
            slotGameobject.GetComponent<Image>().enabled = true;
            slotGameobject.GetComponentInChildren<TextMeshProUGUI>().text = data.GetAmount() + "";
        }
        else
        {
            slotGameobject.GetComponent<Image>().sprite = null;
            slotGameobject.GetComponent<Image>().enabled = false;
            slotGameobject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"From: {eventData.pointerDrag.transform.parent.GetSiblingIndex()}");

        InventoryItemData data = null;

        foreach (InventoryItemData item in instance.inventory)
            if (item.index == eventData.pointerDrag.transform.parent.GetSiblingIndex())
            {
                data = item;
                Debug.Log(data);
            }

        InventoryItemData droppedItem = data;
        if (eventData.pointerDrag.transform.parent.name == gameObject.name)
        {
            return;
        }
        else
        {
            uint dropIndex = (uint)Mathf.Clamp(transform.GetSiblingIndex(), 0, int.MaxValue);

            Debug.Log($"Drop Index: {dropIndex}");

            foreach (InventoryItemData item in instance.inventory)
                if (item.index == dropIndex)
                    return;

            droppedItem.index = dropIndex;

            instance.CMDRefreshUI();
        }
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    transform.parent.GetComponentInChildren<Draggable>().OnPointerDown(eventData);
    //}
    //
    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.parent.GetComponentInChildren<Draggable>().OnDrag(eventData);
    //}
    //
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    transform.parent.GetComponentInChildren<Draggable>().OnPointerUp(eventData);
    //}
}

