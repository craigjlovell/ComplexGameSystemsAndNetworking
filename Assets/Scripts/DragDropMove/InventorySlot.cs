using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IDropHandler//, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject slotGameobject;

    public void UpdateSlot()
    {
        if(Inventory.instance.inventory[transform.parent.GetSiblingIndex()] != null)
        {
            slotGameobject.GetComponent<Image>().sprite = Inventory.instance.inventory[transform.parent.GetSiblingIndex()].itemPrefab.GetComponent<InvImage>().itemImage;
            slotGameobject.GetComponent<Image>().enabled = true;                
            slotGameobject.GetComponentInChildren<TextMeshProUGUI>().text = Inventory.instance.inventory[transform.parent.GetSiblingIndex()].GetAmount() + "";
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
        InventoryItemData dropeditem = Inventory.instance.inventory[eventData.pointerDrag.GetComponent<Draggable>().transform.parent.GetSiblingIndex()];
        if (eventData.pointerDrag.transform.parent.name == gameObject.name)
        {
            return;
        }
        if(Inventory.instance.inventory[transform.parent.GetSiblingIndex()] == null)
        {
            Inventory.instance.inventory[transform.parent.GetSiblingIndex()] = dropeditem;
            Inventory.instance.inventory[eventData.pointerDrag.GetComponent<Draggable>().transform.parent.GetSiblingIndex()] = null;
            Inventory.instance.RefreshUI();
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

