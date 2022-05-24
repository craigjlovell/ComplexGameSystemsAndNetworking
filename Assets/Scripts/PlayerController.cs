using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public List<InventoryItemData> inventory = new List<InventoryItemData>();

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    Animator animator;
    PlayerManager playerManager;

    public Canvas inventoryWidget;
    //public InvManager manager;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] public GameObject Header;
    [SerializeField] private InventoryItemData itemToAdd;
    [SerializeField] private InventoryItemData itemToRemove;

    public GameObject[] slots;

    public uint id { get; set; }

    // Start is called before the first frame update

    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();        
        animator = GetComponent<Animator>();        
    }

    void Start()
    {
        playerManager.AddPlayer(this);
        id = GetComponent<NetworkIdentity>().netId;

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
        RefreshUI();
        float fwd = Input.GetAxis("Vertical");

        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));
        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Tab) && inventoryWidget.enabled == true)
        {
            inventoryWidget.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryWidget.enabled == false)
        {
            inventoryWidget.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = other.GetComponent<ItemLinker>();
        if (item != null)
        {
            inventory.Add(item.itemLinker);
            other.gameObject.SetActive(false);
        }
    }

    public void SetColor(Color col)
    {
        transform.GetChild(0).GetChild(1).GetComponentInChildren<Renderer>().material.color = col;
        transform.GetChild(0).GetChild(2).GetComponentInChildren<Renderer>().material.color = col;
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemImage;
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }
    void Add(InventoryItemData item)
    {
        inventory.Add(item);
    }

    void Remove(InventoryItemData item)
    {
        inventory.Remove(item);
    }



    //public override void OnStartClient()
    //{
    //    
    //    base.OnStartClient();
    //    SetId();
    //}
    //
    //public override void OnStartServer()
    //{
    //    
    //    base.OnStartServer();
    //    SetId();
    //}
    //
    //public override void OnStartLocalPlayer()
    //{
    //    
    //    base.OnStartLocalPlayer();
    //    SetId();
    //}
}
