using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object Item Data", menuName = "Inventory System/Items/EQUIPMENT")]
public class InvItemEquipment : InventoryItemData
{
    public override InvItemBlocks GetBlock() { return null; }
    public override InvItem GetDefault() { return null; }
    public override InvItemFood GetFood() { return null; }
    public override InvItemEquipment GetEquipment() { return this; }
    public void Awake()
    {
        itemType = ItemType.EQUIPMENT;
    }
}