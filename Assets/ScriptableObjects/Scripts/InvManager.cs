using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InvManager : NetworkBehaviour
{
    public InvManager manager;
    public InvSlot slotManager;

    [SerializeField] public GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] public InventoryItemData itemToAdd;
    [SerializeField] public InventoryItemData itemToRemove;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    public GameObject[] slots;

    void Awake()
    {
        if (isLocalPlayer)
            return;

        manager = gameObject.GetComponent<InvManager>();
        slotManager = gameObject.GetComponentInChildren<InvSlot>();
        slots = new GameObject[slotHolder.transform.childCount];

        RefreshUI();
        Add(itemToAdd);
        Remove(itemToRemove);
    }

    void Start()
    {

    }

    void Update()
    {        
        if (!isLocalPlayer)
            return;
       
    }

    public void RefreshUI()
    {
        for(int i = 0; i < slots.Length - 1; i++)
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

    public void Add(InventoryItemData item)
    {
        items.Add(item);
        RefreshUI();
    }

    public void Remove(InventoryItemData item)
    {
        items.Remove(item);
        RefreshUI();
    }

    //[Command]
    //public void CmdAdd()
    //{
    //    RpcAdd();
    //}
    //
    //[Command]
    //public void CmdRemove()
    //{
    //    RpcRemove();
    //}
    //
    //[Command]
    //public void CmdRefreshUI()
    //{
    //    RpcRefreshUI();
    //}
    //
    //[ClientRpc]
    //public void RpcAdd()
    //{
    //    Add(itemToAdd);
    //}
    //
    //[ClientRpc]
    //public void RpcRemove()
    //{
    //    Remove(itemToRemove);
    //}
    //
    //[ClientRpc]
    //public void RpcRefreshUI()
    //{
    //    RefreshUI();
    //}

}
