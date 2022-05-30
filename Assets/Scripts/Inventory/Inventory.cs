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

   //public readonly SyncList<InventoryItemData> inventory = new SyncList<InventoryItemData>();
    public List<InventoryItemData> inventory = new List<InventoryItemData>();

    public GameObject[] slots;

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
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i] + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    [Command]
    public void RpcAdd(ItemLinker item)
    {
        inventory.Add(item.itemDataLinker);
        RpcRefreshUI();
    }

    [Command]
    public void RpcRemove(ItemLinker item)
    {
        inventory.Remove(item.itemDataLinker);
        RpcRefreshUI();
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
}
