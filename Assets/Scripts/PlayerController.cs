using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;
using PlayFab.ClientModels;

public class PlayerController : NetworkBehaviour
{
    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    Animator animator;
    ServerManager serverManager;
    public Inventory inventoryItems;

    public Canvas inventoryWidget;

    public string MyPlayfabID;
    public uint id { get; set; }

    // Start is called before the first frame update

    void Awake()
    {
        serverManager = GameObject.Find("ServerManagerData").GetComponent<ServerManager>();        
        animator = GetComponent<Animator>();
        inventoryItems = GetComponent<Inventory>();
        
    }

    void Start()
    {
        serverManager.AddPlayer(this);
        id = GetComponent<NetworkIdentity>().netId;
        GetAccountInfo();
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

    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }


    void Successs(GetAccountInfoResult result)
    {

        MyPlayfabID = result.AccountInfo.TitleInfo.TitlePlayerAccount.Id;//.PlayFabId;

    }


    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

    public void SetColor(Color col)
    {
        transform.GetChild(0).GetChild(1).GetComponentInChildren<Renderer>().material.color = col;
        transform.GetChild(0).GetChild(2).GetComponentInChildren<Renderer>().material.color = col;        
    }
    
}
