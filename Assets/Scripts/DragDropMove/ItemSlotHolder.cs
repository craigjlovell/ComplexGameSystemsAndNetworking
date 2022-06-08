using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemSlot
{
    public InventoryItemData item;
    public int quantity;

    public ItemSlot(InventoryItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }

    public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }
}