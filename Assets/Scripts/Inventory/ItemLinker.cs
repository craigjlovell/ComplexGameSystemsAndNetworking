using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;


[Serializable]
public class ItemLinker : NetworkBehaviour
{ 
    [SerializeField] private Inventory inventoryItems;
    [SerializeField] private PlayerController player;
    [SerializeField] public InventoryItemData itemData;
    public InventoryItemData GetItem() { return itemData; }

    public void Awake()
    {

    }

    private void Update()
    {
        
    }

    //Todo send command to server that items been added to a specific player 
    public void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<PlayerController>();


        if (player == other.gameObject.GetComponent<PlayerController>() && other == other.GetComponent<CharacterController>())
        {
            inventoryItems = other.GetComponent<Inventory>();
            inventoryItems.Add(GetItem());
            // to add an item to the apporiate player's PlayerData (script) via an id   
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        //removed nasted if 
        if (!isServer) return;
        //if this is just to check if its a player, better check for the tag.
        if (!other.gameObject.GetComponent<PlayerController>() && other == other.GetComponent<CharacterController>()) return;
        
        inventoryItems = other.GetComponent<Inventory>();
        inventoryItems.Add(itemData);
        
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
