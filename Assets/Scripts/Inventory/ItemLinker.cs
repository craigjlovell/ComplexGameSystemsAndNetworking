using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;

public class ItemLinker : MonoBehaviour
{
    InventoryItemData itemData;
    public Inventory inventoryItems;

    PlayerManager playerManager;

    public void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = GetComponent<ItemLinker>();
        PlayerController player = other.GetComponent<PlayerController>();
        inventoryItems = other.GetComponent<Inventory>();

        if (player != null)
        {
            inventoryItems.CmdAdd(item.itemData);
            Destroy(item.gameObject);
            //other.gameObject.SetActive(false);
        }
       
    }
}
