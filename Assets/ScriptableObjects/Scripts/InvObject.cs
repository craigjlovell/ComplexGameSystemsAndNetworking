using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InvObject : ScriptableObject
{
    public List<AddItemToInventorySlot> container = new List<AddItemToInventorySlot>();
    public void AddItem(InventoryItemData a_item, int a_amount)
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemObject == a_item)
            {
                container[i].AddAmount(a_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            container.Add(new AddItemToInventorySlot(a_item, a_amount));
        }
    }
}

[Serializable]
public class AddItemToInventorySlot
{
    public InventoryItemData itemObject;
    public int amount;

    public AddItemToInventorySlot(InventoryItemData a_item, int a_amount)
    {
        itemObject = a_item;
        amount = a_amount;
    }
    public void AddAmount(int a_value)
    {
        amount += a_value;
    }
}