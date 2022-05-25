using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TEST : MonoBehaviour
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
}
