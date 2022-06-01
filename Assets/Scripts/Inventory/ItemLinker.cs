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

        //removed nasted if 
        if (!isServer) return;
        //if this is just to check if its a player, better check for the tag.
        if (other.gameObject.GetComponent<PlayerController>() && other == other.GetComponent<CharacterController>())
        {
            inventoryItems = other.GetComponent<Inventory>();
            inventoryItems.Add(itemData);
        }

        
         
        GiveItemToPlayer(other.GetComponent<NetworkIdentity>().connectionToClient, itemData, other.gameObject);

    }

    [TargetRpc]
    void GiveItemToPlayer(NetworkConnection target, InventoryItemData data, GameObject playerGameObject)
    {
        if(!isClientOnly) return;
        
        playerGameObject.GetComponent<Inventory>();
        inventoryItems.Add(data);

    }

    
}
