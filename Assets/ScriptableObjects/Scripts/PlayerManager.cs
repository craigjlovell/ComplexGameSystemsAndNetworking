using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//This Becomes Manager
public class PlayerManager : NetworkBehaviour
{    
    public List<PlayerData> players = new List<PlayerData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPlayersColors()
    {
        //for (int i = 0; i < players.Count; i++)
        //{
        //    if (players[i] == isLocalPlayer)
        //    {
        //        players[i].SetColor(Color.red);
        //    }
        //    else if (players[i] == isServer)
        //    {
        //        players[i].SetColor(Color.blue);
        //    }
        //}
    }

    public void AddPlayer(PlayerData player)
    {
        players.Add(player);
        SetPlayersColors();
    }

    
}
