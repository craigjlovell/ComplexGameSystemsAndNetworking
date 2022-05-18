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

    ScreenChange screen;
    public InvManager manager;
    public InvSlot slotManager;

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    // Start is called before the first frame update

    void Awake()
    {
        if (isLocalPlayer)
            return;
        
        manager = gameObject.GetComponent<InvManager>();
        slotManager = gameObject.GetComponentInChildren<InvSlot>();
        animator = gameObject.GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {                
        float fwd = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", Mathf.Abs(fwd));
        animator.SetFloat("Sense", Mathf.Sign(fwd));

        animator.SetFloat("Turn", Input.GetAxis("Horizontal"));
        
        //if (!isLocalPlayer)
        //    return;

        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    screen.GameInv1();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    screen.GameInv2();
        //}

    }

    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = other.GetComponent<ItemLinker>();
        if (item != null)
        {
            manager.items.Add(item.itemLinker);
            manager.CmdRefresh();
            Destroy(other.gameObject);
        }            
    }
}
