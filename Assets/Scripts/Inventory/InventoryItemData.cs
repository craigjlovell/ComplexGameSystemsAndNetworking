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

    public string id;
    public ItemType itemType;
    public GameObject item;

    public int stackSizeMax = 100;

    public bool isConsumable;
    public bool isStackable;
}

