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

        //if(!isLocalPlayer)
        //    return;

        slots = new GameObject[slotHolder.gameObject.transform.childCount];
        Header.gameObject.transform.SetAsLastSibling();
        for (int i = 0; i < slotHolder.gameObject.transform.childCount; i++)
        {
            slots[i] = slotHolder.gameObject.transform.GetChild(i).gameObject;
        }

    }

    void Update()
    {
        if (!isServer)
            return;
        
        CmdRefresh();

        if (isLocalPlayer)
            return;

        RefreshUI();        
    }

    [Server]
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemImage;
            }
            catch
            {
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    [Command]
    public void CmdRefresh()
    {
        RpcRefresh();
    }
    [ClientRpc]
    public void RpcRefresh()
    {
        RefreshUI();
    }

    void Add(InventoryItemData item)
    {
        items.Add(item);
    }

    void Remove(InventoryItemData item)
    {
        items.Remove(item);
    }        
}
