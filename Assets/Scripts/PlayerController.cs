using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Animator animator;
    CharacterController cc;

    InvManager manager;

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CmdTrigger();
        if (!isLocalPlayer)
            return;
        

        float fwd = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));

        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
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

    public void OnTriggerEnter(Collider other)
    {
        InventoryItemData item = other.GetComponent<InventoryItemData>();
        if (item != null)
        {
            manager.items.Add(item);
            manager.RefreshUI();
            Destroy(other.gameObject);
        }            
    }
}
