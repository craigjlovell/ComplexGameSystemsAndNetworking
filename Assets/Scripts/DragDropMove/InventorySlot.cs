using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject slotGameobject;

    public void UpdateSlot()
    {
        if(Inventory.instance.inventory[transform.GetSiblingIndex()] != null)
        {
            slotGameobject.GetComponent<Image>().sprite = Inventory.instance.inventory[transform.GetSiblingIndex()].itemPrefab.GetComponent<InvImage>().itemImage;
            //Inventory.instance.slots[transform.GetSiblingIndex()].transform.GetChild(0).GetComponent<Image>().sprite = Inventory.instance.inventory[transform.GetSiblingIndex()].itemPrefab.GetComponent<ItemLinker>().GetComponent<InvImage>().itemImage;
            slotGameobject.SetActive(true);
        }
        else
        {
            slotGameobject.SetActive(false);
        }
    }
}

