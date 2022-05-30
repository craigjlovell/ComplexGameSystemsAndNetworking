using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    Animator animator;
    PlayerManager playerManager;
    Inventory inventoryItems;

    public Canvas inventoryWidget;

    public uint id { get; set; }

    // Start is called before the first frame update

    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();        
        animator = GetComponent<Animator>();
        inventoryItems = GetComponent<Inventory>();
    }

    void Start()
    {
        playerManager.AddPlayer(this);
        id = GetComponent<NetworkIdentity>().netId;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

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
            inventoryItems.RpcAdd(item);
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
        }
    }

    public void SetColor(Color col)
    {
        transform.GetChild(0).GetChild(1).GetComponentInChildren<Renderer>().material.color = col;
        transform.GetChild(0).GetChild(2).GetComponentInChildren<Renderer>().material.color = col;        
    }
    
}
