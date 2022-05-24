using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemLinker : NetworkBehaviour
{
    public InventoryItemData itemLinker;

    public override void OnStopServer()
    {        
        gameObject.SetActive(true);
        base.OnStopServer();
    }

}
