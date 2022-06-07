using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;


public class Inventory : NetworkBehaviour
{
    public static Inventory instance;

    [SerializeField] private ServerManager serverManager;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private InvSlot invSlot;

    [SerializeField] private GameObject slotHolder; 
    [SerializeField] private GameObject header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;


    public List<InventoryItemData> inventory = new List<InventoryItemData>();

    public InventorySlot[] inventorySlots;

    // Start is called before the first frame update
    void Awake()
    {
        serverManager = GameObject.Find("ServerManagerData").GetComponent<ServerManager>();        

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    public void Start()
    {
        inventorySlots = FindObjectsOfType<InventorySlot>();
        serverManager.AddPlayerInventory(playerData);
        RefreshUI();
        //inventorySlots = new GameObject[invSlot.invSlots.Count];
        //inventorySlots.Capacity = invSlot.numOfSlots;
        //for (int i = 0; i < inventorySlots.Capacity - 1; i++)
        //{
        //    inventorySlots.Add(new InventorySlot());
        //
        //    // taking the value form slotinv list whichj is 25 and giving the same to the slotgameobject saying there 25 of them 
        //    inventorySlots[i].slotGameobject = invSlot.invSlots[i];            
        //}        

        //for (int i = 0; i < invSlot.numOfSlots; i++)
        //{       
        //    if (slotHolder.transform.GetChild(i).CompareTag("Slot"))
        //    {
        //        Debug.Log("1 " + slotHolder.transform.GetChild(i).gameObject.name);
        //        Debug.Log("2 " + inventorySlots[i].name);                
        //        inventorySlots[i] = invSlot.invSlots[i];
        //        Debug.Log("3 " + inventorySlots[i].name);
        //        inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;      
        //        inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        //        inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        //    }
        //}
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
                //Old Might be broken without below
                //inventorySlots[i].GetComponent<Image>().sprite = inventory[i].itemPrefab.GetComponent<InvImage>().itemImage;
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;                
                inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].GetAmount() + "";
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
    public bool AddItem(InventoryItemData item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
        }
        return false;
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
