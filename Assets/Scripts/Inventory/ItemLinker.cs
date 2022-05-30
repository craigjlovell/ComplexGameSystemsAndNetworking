using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemLinker : NetworkBehaviour
{
    public InventoryItemData itemDataLinker;
    public Inventory inventoryItems;

    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = GetComponent<ItemLinker>();
        inventoryItems = other.GetComponent<Inventory>();
        if (item != null)
        {
            inventoryItems.CmdAdd(item.itemDataLinker);
            //Destroy(other.gameObject);
            //ddother.gameObject.SetActive(false);
        }
       
    }
}
