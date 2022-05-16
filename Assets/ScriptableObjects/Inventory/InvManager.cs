using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class InvManager :  NetworkBehaviour
{
    [SerializeField] private GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public List<InventoryItemData> items = new List<InventoryItemData>();

    private GameObject[] slots;

    [Client]
    // Start is called before the first frame update
    public void Start()
    {
        Header.transform.SetAsLastSibling();
        slots = new GameObject[slotHolder.transform.childCount];
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        RefreshUI();

        Add(itemToAdd);
        Remove(itemToRemove);
    }

    [Client]
    public void RefreshUI()
    {

        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemImage;
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    [Client]
    public void Add(InventoryItemData item)
    {
        items.Add(item);
        RefreshUI();
    }

    [Client]
    public void Remove(InventoryItemData item)
    {
        items.Remove(item);
        RefreshUI();
    }
}