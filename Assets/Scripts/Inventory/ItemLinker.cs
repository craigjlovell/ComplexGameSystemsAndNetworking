using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;

public class ItemLinker : NetworkBehaviour
{
    public InventoryItemData itemData;
    [SerializeField] private Inventory inventoryItems;
    [SerializeField] private PlayerController player;

    public void Awake()
    {

    }

    private void Update()
    {
        
    }

    //Todo send command to server that items been added to a specific player 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && other == other.GetComponent<CharacterController>())
        {
            inventoryItems = other.GetComponent<Inventory>();

            inventoryItems.Add(itemData);
        }             
    }
}
