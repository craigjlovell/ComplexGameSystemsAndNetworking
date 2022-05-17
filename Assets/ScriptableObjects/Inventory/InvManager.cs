using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InvManager : NetworkBehaviour
{
    [SerializeField] private static InvManager a_instance;
    //[SerializeField] private static InvManager a_instance;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    public GameObject[] slots;

    public static InvManager Instance
    {
        get
        {
            if (a_instance == null)
            {
                GameObject go = new GameObject("Inv");
                a_instance = go.AddComponent<InvManager>();
            }

            return a_instance;
        }
    }

    void Awake()
    {
        a_instance = this;
    }

    void Start()
    {
        base.OnStartServer();
        Header.transform.SetAsLastSibling();
        slots = new GameObject[slotHolder.transform.childCount];
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
        RefreshUI();

        if (!isLocalPlayer)
            return;

        CmdRefreshUI();
        
        CmdAdd();
        CmdRemove();
        
    }

    [Server]
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

    [Server]
    public void Add(InventoryItemData item)
    {
        items.Add(item);
        RefreshUI();
    }

    [Server]
    public void Remove(InventoryItemData item)
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
