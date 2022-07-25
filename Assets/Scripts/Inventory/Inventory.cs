using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;


public class Inventory : NetworkBehaviour
{
    public static Inventory instance;
    private ItemSlot[] itemSlots = new ItemSlot[0];

    public Action OnItemsUpdated = delegate { };
    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

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

        // feed into new array temp array and flip order
        inventorySlots = FindObjectsOfType<InventorySlot>();

        serverManager.AddPlayerInventory(playerData);
        RefreshUI();

        for (int i = 0; i < inventorySlots.Length / 2; i++)
        {
            var tmp = inventorySlots[i];
            inventorySlots[i] = inventorySlots[inventorySlots.Length - i - 1];
            inventorySlots[inventorySlots.Length - i - 1] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RefreshUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItemData data = null;

            foreach (InventoryItemData item in inventory)
                if (item.index == i)
                    data = item;

            inventorySlots[i].UpdateSlot(data);
        }
    }

    //public InventoryItemData Contains(InventoryItemData item)
    //{
    //    for (int i = 0; i < inventory.Count; i++)
    //    {
    //        if (inventory[i] == item)
    //            return inventory[i];
    //    }                          
    //    return null;
    //}

    public bool Add(InventoryItemData item)
    {
        //inventory.Add(item);
        //RefreshUI();

        if (inventory.Contains(item) && item.isStackable)
        {
            item.AddAmount(1);
        }
        else
        {
            if (inventory.Count < inventorySlots.Length)
            {
                int newIndex = 0;

                for (int i = 0; i <= inventory.Count; i++)
                {
                    bool indexTaken = false;

                    foreach (InventoryItemData data in inventory)
                    {
                        if (data.index == i)
                        {   
                            indexTaken = true;
                            break;
                        }
                    }

                    if (!indexTaken)
                    {
                        newIndex = i;
                        break;
                    }
                }

                item.index = (uint)Mathf.Clamp(newIndex, 0, int.MaxValue);

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

    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];

        if (firstSlot == secondSlot) { return; }

        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.stackSizeMax - secondSlot.quantity;

                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;

                    itemSlots[indexOne] = new ItemSlot();

                    OnItemsUpdated.Invoke();

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        OnItemsUpdated.Invoke();
    }

}
