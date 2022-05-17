using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InvManager :  NetworkBehaviour
{
    [SerializeField] private GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    private GameObject[] slots;
    // Start is called before the first frame update
    
    void Start()
    {
        Header.transform.SetAsLastSibling();
        slots = new GameObject[slotHolder.transform.childCount];
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        //CmdRefreshUI();
        //
        //CmdAdd();
        //CmdRemove();
        RefreshUI();
        Add(itemToAdd);
        Remove(itemToRemove);
    }
    //void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;
    //
    //    CmdRefreshUI();
    //
    //    CmdAdd();
    //    CmdRemove();
    //}

    [Client]
    public void RefreshUI()
    {
        for(int i = 1; i < slots.Length - 1; i++)
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

    [Client]
    public void Add(InventoryItemData item)
    {
        items.Add(item);
        RefreshUI();
    }

    [Client]
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

    //[Command]
    //public void CmdRemove()
    //{
    //    RpcRemove();
    //}

    //[Command]
    //public void CmdRefreshUI()
    //{
    //    RpcRefreshUI();
    //}

    //[ClientRpc]
    //public void RpcAdd()
    //{
    //    Add(itemToAdd);
    //}

    //[ClientRpc]
    //public void RpcRemove()
    //{
    //    Remove(itemToRemove);
    //}

    //[ClientRpc]
    //public void RpcRefreshUI()
    //{
    //    RefreshUI();
    //}
}
