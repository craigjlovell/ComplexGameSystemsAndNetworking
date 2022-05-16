using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    Animator animator;
    CharacterController cc;

    InventoryItemData inventory;
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
        if (!isLocalPlayer)
            return;

        float fwd = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));

        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item != null)
        {
            if(inventory.itemType == ItemType.DEFAULT || inventory.itemType == ItemType.FOOD || inventory.itemType == ItemType.EQUIPMENT || inventory.itemType == ItemType.BLOCKS)
            {
                manager.Add(item.item);
            }
            Destroy(other.gameObject);
        }
    }
}
