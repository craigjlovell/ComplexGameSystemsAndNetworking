using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    Animator animator;
    CharacterController cc;
    PlayerManager playerManager;



    Canvas screen;
    public InvManager manager;
    public InvSlot slotManager;

    // Start is called before the first frame update

    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        manager = GameObject.Find("InventoryManager").GetComponent<InvManager>();

        animator = gameObject.GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
        slotManager = gameObject.GetComponent<InvSlot>();        
        screen = null;
    }

    void Start()
    {
        //playerManager.AddPlayer(this);
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            screen = GameObject.FindGameObjectWithTag("Game").GetComponent<Canvas>();
            if (screen.enabled == true)
            {
                screen = null;
                Time.timeScale = 0;
                screen = GameObject.FindGameObjectWithTag("Game").GetComponent<Canvas>();
                screen.enabled = false;
                screen = this.gameObject.GetComponentInChildren<Canvas>();
                screen.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                screen = this.gameObject.GetComponentInChildren<Canvas>();
                screen.enabled = false; 
                screen = GameObject.FindGameObjectWithTag("Game").GetComponent<Canvas>();
                screen.enabled = true;
            }                
        }        
    }    

    public void OnTriggerEnter(Collider other)
    {
        ItemLinker item = other.GetComponent<ItemLinker>();
        if (item != null)
        {
            manager.items.Add(item.itemLinker);
            Destroy(other.gameObject);
        }            
    }

    public void SetColor(Color col)
    {
        cc.transform.GetChild(0).GetChild(1).GetComponentInChildren<Renderer>().material.color = col;
        cc.transform.GetChild(0).GetChild(2).GetComponentInChildren<Renderer>().material.color = col;
    }
}
