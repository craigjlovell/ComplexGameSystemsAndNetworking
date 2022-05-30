using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class Inventory : NetworkBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;
    InventoryItemData itemData;
    InventoryItemData itemLinker;

    [SerializeField] private int amount;

    public InventoryItemData GetItem() { return itemData; }
    public int GetAmount() { return amount; }
    public void SetAmount(int a_stackAmount) { amount += a_stackAmount; }

    //public readonly SyncList<InventoryItemData> inventory = new SyncList<InventoryItemData>();
    public List<InventoryItemData> inventory = new List<InventoryItemData>();

    public GameObject[] slots;

    public Inventory()
    {
        itemData = null;
        amount = 0;
    }

    public Inventory(InventoryItemData a_item, int a_stackAmount)
    {
        itemData = a_item;
        amount = a_stackAmount;
    }


    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        Header.transform.SetAsLastSibling();
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {

        }
    }

    [ClientRpc]
    void RpcRefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemImage;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetAmount() + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public InventoryItemData Contains(InventoryItemData item)
    {
        foreach (InventoryItemData slot in inventory)
        {
            if (GetItem() == item)
                return slot;
        }
        return null;
    }

    [Server]
    void Add(InventoryItemData item)
    {
        inventory.Add(item);
        //SetAmount(1);
        RpcRefreshUI();

        //InventoryItemData slot = Contains(item);
        //if (slot != null)
        //    SetAmount(1);
        //else
        //{
        //    inventory.AddAdd(new InventoryItemData(item, 1))
        //}
        //RpcRefreshUI();
    }

    [Server]
    void Remove(InventoryItemData item)
    {
        inventory.Remove(item);
        RpcRefreshUI();
    }

    [ClientRpc]
    public void RpcAdd(InventoryItemData item)
    {
        Add(item);
    }

    [ClientRpc]
    public void RpcRemove(InventoryItemData item)
    {
        Remove(item);
    }

    [Command]
    public void CmdAdd(InventoryItemData item)
    {
        RpcAdd(item);
    }
    
    [Command]
    public void CmdRemove(InventoryItemData item)
    {
        RpcRemove(item);
    }
}
