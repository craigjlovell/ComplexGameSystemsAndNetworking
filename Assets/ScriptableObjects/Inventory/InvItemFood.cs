using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object Item Data", menuName = "Inventory System/Items/FOOD")]
public class InvItemFood : InventoryItemData
{
    public override InvItemBlocks GetBlock() { return null; }
    public override InvItem GetDefault() { return null; }
    public override InvItemFood GetFood() { return this; }
    public override InvItemEquipment GetEquipment() { return null; }

    public void Awake()
    {
        itemType = ItemType.FOOD;
    }
}
