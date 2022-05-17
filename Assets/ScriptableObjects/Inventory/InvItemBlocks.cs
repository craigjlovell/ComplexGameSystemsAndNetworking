using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block Object Item Data", menuName = "Inventory System/Items/BLOCKS")]
public class InvItemBlocks : InventoryItemData
{
    public override InvItemBlocks GetBlock() { return this; }
    public override InvItem GetDefault() { return null; }
    public override InvItemFood GetFood() { return null; }
    public override InvItemEquipment GetEquipment() { return null; }
    public void Awake()
    {
        itemType = ItemType.BLOCKS;
    }
}