using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object Item Data", menuName = "Inventory System/Items/FOOD")]
public class InvItemFood : InventoryItemData
{

    public void Awake()
    {
        itemType = ItemType.FOOD;
    }
}
