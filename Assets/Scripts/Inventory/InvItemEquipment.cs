using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object Item Data", menuName = "Inventory System/Items/EQUIPMENT")]
public class InvItemEquipment : InventoryItemData
{
    public void Awake()
    {
        itemType = ItemTypes.EQUIPMENT;
    }
}