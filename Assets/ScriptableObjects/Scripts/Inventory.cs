using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Inventory : NetworkBehaviour
{
    public InvManager inventoryManager;
    public InvSlot slotManager;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public GameObject[] slots;

    // Start is called before the first frame update
    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InvManager>();
        slotManager = slotHolder.gameObject.GetComponent<InvSlot>();
    }
    void Start()
    {                
        slots = new GameObject[slotHolder.gameObject.transform.childCount];
        Header.gameObject.transform.SetAsLastSibling();

        for (int i = 0; i < slotHolder.gameObject.transform.childCount; i++)
        {
            slots[i] = slotHolder.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = inventoryManager.items[i].itemImage;
            }
            catch
            {
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    void Add(InventoryItemData item)
    {
        inventoryManager.items.Add(item);
    }

    void Remove(InventoryItemData item)
    {
        inventoryManager.items.Remove(item);
    }
}
