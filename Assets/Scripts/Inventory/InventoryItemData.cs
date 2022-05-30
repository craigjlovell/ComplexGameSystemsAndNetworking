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
    public Sprite itemImage;

    public int stackSizeMax = 100;

    public bool isConsumable;
    public bool isStackable;

    //public abstract InvItemBlocks GetBlock();
    //public abstract InvItem GetDefault();
    //public abstract InvItemFood GetFood();
    //public abstract InvItemEquipment GetEquipment();

    public void CanStack()
    {
        if (isStackable)
        { 

        }
    }
}

