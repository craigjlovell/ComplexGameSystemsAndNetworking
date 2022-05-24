using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//This Becomes Manager
public class PlayerManager : NetworkBehaviour
{    
    public List<PlayerController> players = new List<PlayerController>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
     
        players.Clear();

    }
}
