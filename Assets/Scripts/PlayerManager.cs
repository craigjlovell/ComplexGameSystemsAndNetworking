using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

//This Becomes Manager
public class PlayerManager : NetworkBehaviour
{    
    public List<PlayerController> players = new List<PlayerController>();

    void SetPlayersColors()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == isClient)
            {
                players[i].SetColor(Color.red);
            }
            if (players[i] == players[0])
            {
                players[i].SetColor(Color.blue);
            }
        }        
    }

    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
        SetPlayersColors();
    }

    public void RemovePlayer(PlayerController player)
    {
        players.Remove(player);
    }

    public override void OnStopServer()
    {
        players.Clear();
        SceneManager.UnloadScene("SinglePlayer");
        SceneManager.LoadScene("SinglePlayer");
        base.OnStopServer();
    }
    
    public override void OnStartServer()
    {                
        base.OnStartServer();
    }

    public override void OnStopClient()
    {
        players.Clear();
        SceneManager.LoadSceneAsync("SinglePlayer");
        base.OnStopClient();
    }
    
    public override void OnStartClient()
    {        
        base.OnStartClient();
    }

    //public override void OnStartClient()
    //{
    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        players[i].inventoryWidget = players[i].transform.GetChild(2).gameObject.GetComponent<Canvas>();
    //    }
    //}
}
