using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class InvManager : NetworkBehaviour
{
    public InvManager manager;
    public InvSlot slotManager;

    [SerializeField] public GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    public GameObject[] slots;

    void Awake()
    {
        if (isLocalPlayer)
            return;

        manager = gameObject.GetComponent<InvManager>();
        slotManager = gameObject.GetComponentInChildren<InvSlot>();
        

        
    }

    void Start()
    {
        //var count = 0;
        //foreach (var slotHolderSlot in slotHolder.transform)
        //{
        //    count++;
        //    Debug.Log("Slots: " + count);
        //}
        if(!isLocalPlayer)
            return;
        slots = new GameObject[slotHolder.transform.childCount];
        Header.transform.SetAsLastSibling();
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();
        Add(itemToAdd);
        Remove(itemToRemove);

    }

    void Update()
    {        
        if (!isLocalPlayer)
            return; 
        CmdRefreshUI();
    }

    [Server]
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemImage;

            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }

        }
    }

    [Server]
    void Add(InventoryItemData item)
    {
        items.Add(item);
        RefreshUI();
    }

    [Server]
    void Remove(InventoryItemData item)
    {
        items.Remove(item);
        RefreshUI();
    }

    [Command]
    public void CmdAdd()
    {
        RpcAdd();
    }

    [Command]
    public void CmdRemove()
    {
        RpcRemove();
    }
        
    [Command]
    public void CmdRefreshUI()
    {
        RpcRefreshUI();
    }
    
    [ClientRpc]
    public void RpcAdd()
    {
        Add(itemToAdd);
    }
    
    [ClientRpc]
    public void RpcRemove()
    {
        Remove(itemToRemove);
    }
    
    [ClientRpc]
    public void RpcRefreshUI()
    {
        RefreshUI();
    }
    
}
