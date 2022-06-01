using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    FOOD,
    BLOCKS,
    EQUIPMENT,
    DEFAULT
}

public class InventoryItemData : ScriptableObject
{

    public string itemID;
    public ItemType itemType;
    public GameObject itemPrefab;

    public int stackSizeMax = 100;
    public int stackSizeMin = 0;

    public bool isConsumable;
    public bool isStackable;
}

