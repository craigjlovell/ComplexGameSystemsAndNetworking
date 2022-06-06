using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

[Serializable]
public class TEST : NetworkBehaviour
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackAmount;

    public TEST()
    {
        itemData = null;
        stackAmount = 0;
    }

    public TEST(InventoryItemData a_item, int a_stackAmount)
    {
        itemData = a_item;
        stackAmount = a_stackAmount;
    }

    public InventoryItemData GetItem() { return itemData; }
    public int GetAmount() { return stackAmount; }
    public void SetAmount(int a_stackAmount) { stackAmount += a_stackAmount; }
    public void SubAmount(int a_stackAmount) { stackAmount -= a_stackAmount; }
}
