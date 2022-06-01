using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Mirror;

public class Login : NetworkBehaviour
{
    [SerializeField] private GameObject signInDsiplay = default;
    [SerializeField] private TMP_InputField usernameInputField = default;
    [SerializeField] private TMP_InputField passwordInputField = default;

    public static string SessionTicket;
    public static string EntityID;

    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = usernameInputField.text,
            Password = passwordInputField.text,
            RequireBothUsernameAndEmail = false                     
        }, result =>
        {
            SessionTicket = result.SessionTicket;
            EntityID = result.EntityToken.Entity.Id;
            signInDsiplay.SetActive(false);
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    
    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = usernameInputField.text,
            Password = passwordInputField.text
        }, result => 
        {
            SessionTicket = result.SessionTicket;
            EntityID = result.EntityToken.Entity.Id;
            signInDsiplay.SetActive(false);
        }, error => 
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
