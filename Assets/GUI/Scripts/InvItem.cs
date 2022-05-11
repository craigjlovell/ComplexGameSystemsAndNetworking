using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

[CreateAssetMenu(fileName = "New Default Object Item Data", menuName = "Inventory System/Items/DEFAULT")]
public class InvItem : InventoryItemData
{
    public void Awake()
    {
        itemType = ItemType.DEFAULT;
    }
}
