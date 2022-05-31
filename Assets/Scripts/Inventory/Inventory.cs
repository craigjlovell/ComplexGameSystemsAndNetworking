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

    [SerializeField] private int amount;

   // public InventoryItemData GetItem() { return itemData; }
    public int GetAmount() { return amount; }
    public void SetAmount(int a_stackAmount) { amount += a_stackAmount; }

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

    }

    [ClientRpc]
    void RpcRefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemImage;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetAmount() + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                //slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    //public InventoryItemData Contains(InventoryItemData item)
    //{
    //    foreach (InventoryItemData slot in inventory)
    //    {
    //        if (GetItem() == item)
    //            return slot;
    //    }
    //    return null;
    //}

    public void Add(InventoryItemData item)
    {
        inventory.Add(item);
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

    public void Remove(InventoryItemData item)
    {
        inventory.Remove(item);
        RpcRefreshUI();
    }

}
