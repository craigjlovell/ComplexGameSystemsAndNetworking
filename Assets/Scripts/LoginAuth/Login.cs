using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject signInDsiplay = default;
    [SerializeField] private TMP_InputField usernameInputField = default;
    [SerializeField] private TMP_InputField emailInputField = default;
    [SerializeField] private TMP_InputField passwordInputField = default;

    public static string SessionTicket;

    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = usernameInputField.text,
            Email = emailInputField.text,
            Password = passwordInputField.text
        }, result =>
        {
            SessionTicket = result.SessionTicket;
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
            signInDsiplay.SetActive(false);
        }, error => 
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
