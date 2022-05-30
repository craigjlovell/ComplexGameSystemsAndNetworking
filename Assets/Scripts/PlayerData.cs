using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerData : NetworkBehaviour
{
    public List<InventoryItemData> inventories = new List<InventoryItemData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
        {
            
        }
    }
    
}

