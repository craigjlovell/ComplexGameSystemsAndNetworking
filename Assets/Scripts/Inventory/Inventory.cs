using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class Inventory : NetworkBehaviour
{
    public static Inventory instance;
    ServerManager serverManager;
    PlayerData playerData;
    ItemLinker itemLinker;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;


    public List<InventoryItemData> inventory = new List<InventoryItemData>();

    public InventorySlot[] inventorySlots;

    // Start is called before the first frame update
    void Awake()
    {
        serverManager = GameObject.Find("ServerManagerData").GetComponent<ServerManager>();
    }

    void Start()
    {
        serverManager.AddPlayerInventory(playerData);
        inventorySlots = new InventorySlot[slotHolder.transform.childCount];
        Header.transform.SetAsLastSibling();
        RefreshUI();
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            if (slotHolder.transform.GetChild(i).CompareTag("Slot"))
            {
                inventorySlots[i].icon = slotHolder.transform.GetChild(i).gameObject;                  
                inventorySlots[i].icon.transform.GetChild(0).GetComponent<Image>().sprite = null;      
                inventorySlots[i].icon.transform.GetChild(0).GetComponent<Image>().enabled = false;
                inventorySlots[i].icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshUI()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {

                inventorySlots[i].UpdateSlot();
                //slots[i].GetComponent<Image>().sprite = Inventory.instance.inventory[transform.GetSiblingIndex()].itemPrefab.GetComponent<InvImage>().itemImage;
                //Old Might be broken without below
                //slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemPrefab.GetComponent<ItemLinker>().GetComponent<InvImage>().itemImage;
                //slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetAmount() + "";
            }
        }
    }

    public InventoryItemData Contains(InventoryItemData item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == item)
                return inventory[i];
        }                          
        return null;
    }

    public bool Add(InventoryItemData item)
    {
        //inventory.Add(item);
        //RefreshUI();
        if (Contains(item) && item.isStackable)
        {
            Contains(item).AddAmount(1);
        }
        else
        {
            if (inventory.Count <= inventorySlots.Length)
            {
                inventory.Add(item);
                item.ResetAmount(1);
            }
            else
            {
                RefreshUI();
                return false;
            }
        }
        RefreshUI();
        return true;
    }

    public void Remove(InventoryItemData item)
    {
        inventory.Remove(item);
        RefreshUI();

        //ItemLinker temp = Contains(item);
        //if (temp != null)
        //{
        //    if (temp.GetAmount() > 1)
        //        temp.SubAmount(1);
        //
        //    else
        //    {
        //        ItemLinker slotToRemove = new ItemLinker();
        //
        //        foreach (ItemLinker slot in inventory)
        //        {
        //            if (slot.GetItem() == item)
        //            {
        //                slotToRemove = slot;
        //                break;
        //            }
        //        }
        //        inventory.Remove(slotToRemove);
        //    }
        //}
        //else
        //{
        //    return false;
        //}
        //RefreshUI();
        //return true;
    }
    
    //public void DropItem(InventoryItemData item)
    //{
    //    GameObject o = Instantiate(item.itemPrefab, transform.position + Vector3.up + transform.forward, Quaternion.LookRotation(transform.forward));
    //    ItemLinker p = o.GetComponent<ItemLinker>();
    //
    //    NetworkServer.Spawn(item.itemPrefab);
    //}

}
