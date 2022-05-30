using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block Object Item Data", menuName = "Inventory System/Items/BLOCKS")]
public class InvItemBlocks : InventoryItemData
{
    public void Awake()
    {
        itemType = ItemType.BLOCKS;
    }
}