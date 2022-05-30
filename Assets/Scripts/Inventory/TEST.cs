//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using Mirror;

//[Serializable]
//public class TEST : NetworkBehaviour
//{
//    [SerializeField] private InventoryItemData itemData;
//    [SerializeField] private int stackAmount;

//    public TEST()
//    {
//        itemData = null;
//        stackAmount = 0;
//    }

//    public TEST(InventoryItemData a_item, int a_stackAmount)
//    {
//        itemData = a_item;
//        stackAmount = a_stackAmount;
//    }

//    public InventoryItemData GetItem() { return itemData; }
//    public int GetAmount() { return stackAmount; }
//    public void SetAmount(int a_stackAmount) { stackAmount += a_stackAmount; }
//    public void SubAmount(int a_stackAmount) { stackAmount -= a_stackAmount; }
//}



//public TEST Contains(InventoryItemData item)
//{
//    foreach (TEST slot in testInventory)
//    {
//        if (slot.GetItem() == item)
//            return slot;
//    }

//    return null;
//}

//public void Add(InventoryItemData item)
//{
//    //inventory.Add(item);

//    TEST slot = Contains(item);
//    if (slot != null)
//        slot.SetAmount(1);
//    else
//    {
//        testInventory.Add(new TEST(item, 1));
//    }

//    RefreshUI();
//}

//public bool Remove(InventoryItemData item)
//{
//    //inventory.Remove(item);


//    TEST temp = Contains(item);
//    if (temp != null)
//    {
//        if (temp.GetAmount() > 1)
//            temp.SubAmount(1);

//        else
//        {
//            TEST slotToRemove = new TEST();

//            foreach (TEST slot in testInventory)
//            {
//                if (slot.GetItem() == item)
//                {
//                    slotToRemove = slot;
//                    break;
//                }
//            }
//            testInventory.Remove(slotToRemove);
//        }
//    }
//    else
//    {
//        return false;
//    }
//    RefreshUI();
//    return true;
//}   
//}