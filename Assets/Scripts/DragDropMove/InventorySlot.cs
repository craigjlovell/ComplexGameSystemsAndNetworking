using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject icon;

    public void Start()
    {

    }
    public void UpdateSlot()
    {
        if(Inventory.instance.inventory[transform.GetSiblingIndex()] != null)
        {
            icon.transform.GetChild(0).GetComponent<Image>().sprite = Inventory.instance.inventory[transform.GetSiblingIndex()].itemPrefab.GetComponent<ItemLinker>().GetComponent<InvImage>().itemImage;
            //Inventory.instance.inventorySlots[transform.GetSiblingIndex()].transform.GetChild(0).GetComponent<Image>().sprite = Inventory.instance.inventory[transform.GetSiblingIndex()].itemPrefab.GetComponent<ItemLinker>().GetComponent<InvImage>().itemImage;
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
}

