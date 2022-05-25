using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class Inventory : NetworkBehaviour
{   
    PlayerManager playerManager;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public List<TEST> testInventory = new List<TEST>();
    public List<InventoryItemData> testdata = new List<InventoryItemData>();
    public List<ItemLinker> testlinker = new List<ItemLinker>();

    //old list testing new replace if nothing works
    //public List<GameObject> inventory = new List<GameObject>();
    public GameObject[] slots;

    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }
    void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        Header.transform.SetAsLastSibling();
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = testInventory[i].GetItem().itemImage;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = testInventory[i].GetAmount() + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void Add(InventoryItemData item)
    {
        //inventory.Add(item);

        TEST slot = Contains(item);
        if (slot != null)
            slot.SetAmount(1);
        else
        {
            testInventory.Add(new TEST(item, 1));
        }

        RefreshUI();
    }

    public void Remove(InventoryItemData item)
    {
        //inventory.Remove(item);

        TEST slotToRemove = new TEST();
        foreach (TEST slot in testInventory)
        {
            if (slot.GetItem() == item)
            {
                slotToRemove = slot;
                break;
            }
        }
        testInventory.Remove(slotToRemove);
        RefreshUI();
    }

    public TEST Contains(InventoryItemData item)
    {
        foreach(TEST slot in testInventory)
        {
            if(slot.GetItem() == item)
                return slot;
        }

        return null;
    }    
}
