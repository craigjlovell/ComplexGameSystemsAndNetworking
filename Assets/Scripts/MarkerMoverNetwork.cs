using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// this illustrates the spawned player prefab taking control of an object outside of it via Commands
// the return message (host to client) is handled via a NetworkTransform automatically
public class MarkerMoverNetwork : NetworkBehaviour
{
    Transform marker;
    // Start is called before the first frame update
    void Start()
    {
        // the scene has a couple of marker points - red for the client, blue for the server.
        // mouse clicks place the marker point for your marker. We need each character to point to
        // the correct marker on each machine, and dye each player to match their marker
        // isServer - true if you'r the host instance of the game
        // isLocalPlayer - true if you're in control of this CharacterMovement

        if(isServer != isLocalPlayer)
        {
            // you're the client and this is you, or you're the host and this is not you,
            // therefore its the client character!
            // TODO use better logic to accomodate 3+ players, like
            // id = GetComponent<NetworkIdentity>().playerControllerId;
            marker = GameObject.Find("ClientMarker").transform;
            SetColor(Color.red);
            gameObject.name = "Red Player";
        }
        else
        {
            marker = GameObject.Find("ServerMarker").transform;
            SetColor(Color.blue);
            gameObject.name = "Blue Player";
        }
    }

    void Update()
    {
        // other player's characters still get an update - don't try to move them, just you!
        if (!isLocalPlayer)
            return;

        // click to move a marker
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CmdSetMarker(hit.point);
            }
        }
    }
    void SetColor(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }

    [Command]
    void CmdSetMarker(Vector3 pos)
    {
        RpcMarker(pos);
    }

    [ClientRpc]
    void RpcMarker(Vector3 pos)
    {
        marker.position = pos;
    }
}
