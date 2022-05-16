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

public abstract class InventoryItemData : ScriptableObject
{
    public string id;
    public ItemType itemType;
    public GameObject item;
    public Sprite itemImage;

    public bool isConsumable;
    public bool isStackable;

}

