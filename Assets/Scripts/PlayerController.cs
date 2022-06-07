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
    public TextMesh playerNameText;
    public GameObject floatingInfo;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    Login login;
    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 180.0f;
    public int index = 1;

    Animator animator;
    ServerManager serverManager;

    public Canvas inventoryWidget;

    public string MyPlayfabID;
    public uint id { get; set; }

    // Start is called before the first frame update

    void Awake()
    {
        serverManager = GameObject.Find("ServerManagerData").GetComponent<ServerManager>();        
        animator = GetComponent<Animator>();
        login = GameObject.Find("Login").GetComponent<Login>();
    }

    void Start()
    {
        serverManager.AddPlayer(this);
        id = GetComponent<NetworkIdentity>().netId;
        //MyPlayfabID = login.EntityID;
        
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

    public override void OnStartLocalPlayer()
    {
        string name = "Player " + login.EntityID;
        CmdSetupPlayer(name);
    }

    public void GetAccountInfo()
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

    [Command]
    public void CmdSetupPlayer(string a_name)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = a_name;
    }

    void OnNameChanged(string a_old, string a_new)
    {
        playerNameText.text = playerName;
    }

    public void SetColor(Color col)
    {
        transform.GetChild(0).GetChild(1).GetComponentInChildren<Renderer>().material.color = col;
        transform.GetChild(0).GetChild(2).GetComponentInChildren<Renderer>().material.color = col;        
    }
    
}
