using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    //public GameObject inv;
    //public GameObject slot;

    Animator animator;
    CharacterController cc;

    public InvManager manager;
    public InvSlot slotManager;

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    // Start is called before the first frame update

    private void Awake()
    {
        if (isLocalPlayer)
            return;
        
        manager = gameObject.GetComponent<InvManager>();
        slotManager = gameObject.GetComponentInChildren<InvSlot>();               
    }

    void Start()
    {
        
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        if (!isLocalPlayer)
            return;
        //inv = GameObject.Find("Inv");
        //slot = GameObject.Find("InventoryGUI");        
        
        //manager = inv.gameObject.GetComponent<InvManager>();
        //slotManager = slot.gameObject.GetComponent<InvSlot>();
    }

    // Update is called once per frame
    void Update()
    {                
        float fwd = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));

        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
        
        if (!isLocalPlayer)
            return;

        CmdTrigger();
    }

    [Command]
    public void CmdTrigger()
    {
        RpcTrigger();
    }
    
    [ClientRpc]
    public void RpcTrigger()
    {
        OnTriggerEnter(cc);
    }

    [Server]
    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = other.GetComponent<ItemLinker>();
        if (item != null)
        {
            manager.manager.items.Add(item.itemLinker);
            Destroy(other.gameObject);
        }            
    }
}
