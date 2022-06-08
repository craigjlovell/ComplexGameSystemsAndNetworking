using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemTypes
{
    EMPTY,
    FOOD,
    BLOCKS,
    EQUIPMENT,
    DEFAULT
}

public class InventoryItemData : ScriptableObject
{
    public uint index;
    public string itemID;
    public ItemTypes itemType;
    public GameObject itemPrefab;

    public int stackSizeMax = 100;
    public int stackSizeMin = 0;

    public int quantity;

    public bool isConsumable;
    public bool isStackable;

    public InventoryItemData()
    {
        quantity = 0;
    }

    public InventoryItemData(int a_stackAmount)
    {
        quantity = a_stackAmount;
    }
    
    public int GetAmount() { return quantity; }
    public void AddAmount(int a_stackAmount) { quantity += a_stackAmount; }
    public void SubAmount(int a_stackAmount) { quantity -= a_stackAmount; }
    public void ResetAmount(int a_stackAmount) { quantity = a_stackAmount; }
}



