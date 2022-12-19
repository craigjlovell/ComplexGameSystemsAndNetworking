using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;


public class Inventory : NetworkBehaviour
{
    public Inventory instance;
    private ItemSlot[] itemSlots = new ItemSlot[0];

    public Action OnItemsUpdated = delegate { };
    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

    [SerializeField] private ServerManager serverManager;
    [SerializeField] private PlayerData playerData;


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
        CMDRefreshUI();

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
        if (!isLocalPlayer)
            return;
        CMDRefreshUI();
    }

    [ClientRpc]
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

    [Command]
    public void CMDRefreshUI()
    {
        RefreshUI();
    }
    [TargetRpc]
    public void CcbRefreshUI()
    {
        RefreshUI();
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
                CMDRefreshUI();
                return false;
            }

        }
        CMDRefreshUI();
        return true;
    }

    public bool Remove(InventoryItemData item)
    {
        InventoryItemData temp = item;
        if (inventory.Contains(item) != null)
        {
            if (temp.GetAmount() > 1)
                temp.SubAmount(1);
        
            else
            {
                InventoryItemData slotToRemove = new InventoryItemData();
        
                foreach (InventoryItemData slot in inventory)
                {
                    if (slot.index == item.index)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }
                inventory.Remove(slotToRemove);
            }
        }
        else
        {
            RefreshUI();
            return false;
        }
        RefreshUI();
        return true;
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

    //public void MAdd(InventoryItemData item)
    //{

    //    if (inventory.Contains(item) && item.isStackable)
    //    {
    //        item.AddAmount(1);
    //    }
    //    else
    //    {
    //        if (inventory.Count < inventorySlots.Length)
    //        {
    //            int newIndex = 0;

    //            for (int i = 0; i <= inventory.Count; i++)
    //            {
    //                bool indexTaken = false;

    //                foreach (InventoryItemData data in inventory)
    //                {
    //                    if (data.index == i)
    //                    {
    //                        indexTaken = true;
    //                        break;
    //                    }
    //                }

    //                if (!indexTaken)
    //                {
    //                    newIndex = i;
    //                    break;
    //                }
    //            }

    //            item.index = (uint)Mathf.Clamp(newIndex, 0, int.MaxValue);

    //            inventory.Add(item);
    //            item.ResetAmount(1);
    //        }
    //        else
    //        {
    //            CMDRefreshUI();
    //        }

    //    }
    //    CMDRefreshUI();
    //}

    //[ClientRpc]
    //public void MRemove(InventoryItemData item)
    //{
    //    InventoryItemData temp = item;
    //    if (inventory.Contains(item) != null)
    //    {
    //        if (temp.GetAmount() > 1)
    //            temp.SubAmount(1);

    //        else
    //        {
    //            InventoryItemData slotToRemove = new InventoryItemData();

    //            foreach (InventoryItemData slot in inventory)
    //            {
    //                if (slot.index == item.index)
    //                {
    //                    slotToRemove = slot;
    //                    break;
    //                }
    //            }
    //            inventory.Remove(slotToRemove);
    //        }
    //    }
    //    else
    //    {
    //        CMDRefreshUI();
    //    }
    //    CMDRefreshUI();
    //}

    //[Command]
    //public void CmdRem(InventoryItemData item)
    //{
    //    MRemove(item);
    //}

    //[ClientRpc]
    //public void rpcadd(InventoryItemData item)
    //{
    //    MAdd(item);
    //}

    //[Command]
    //public void CmdAdd(InventoryItemData item)
    //{
    //    rpcadd(item);
    //}

}
