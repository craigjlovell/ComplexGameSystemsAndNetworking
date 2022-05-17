using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

[CreateAssetMenu(fileName = "New Default Object Item Data", menuName = "Inventory System/Items/DEFAULT")]
public class InvItem : InventoryItemData
{
    public override InvItemBlocks GetBlock() { return null; }
    public override InvItem GetDefault() { return this; }
    public override InvItemFood GetFood() { return null; }
    public override InvItemEquipment GetEquipment() { return null; }
    public void Awake()
    {
        itemType = ItemType.DEFAULT;
    }
}
