using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PlayFab;
using kcp2k;

public class UnityNetworkServer : NetworkManager
{
    public event Action<string> OnPlayerAdded;
    public event Action<string> OnPlayerRemoved;

    public List<UnityNetworkConnection> Connections { get; set; }

    [SerializeField] Configuration _configuration = default;
    public Configuration Config
    {
        get
        {
            return _configuration;
        }
    }

    public KcpTransport Transport
    {
        get
        {
            return transport as KcpTransport;
        }
        set
        {
            transport = value;
        }
    }

    public static Action<NetworkConnection> OnClientDisconnected { get; internal set; }
    public static Action<NetworkConnection> OnClientConnected { get; internal set; }

    public override void Awake()
    {
        base.Awake();

        if (Config.buildType == BuildType.REMOTE_SERVER)
        {
            Connections = new List<UnityNetworkConnection>();
            NetworkServer.RegisterHandler<ReceiveAuthenticateMessage>(OnRecieveAuthenticate);
        }
    }

    private void OnRecieveAuthenticate(NetworkConnection _conn, ReceiveAuthenticateMessage msgType)
    {
        var conn = Connections.Find(c => c.ConnectionId == _conn.connectionId);
        if (conn != null)
        {
            conn.PlayFabId = msgType.PlayFabId;
            conn.IsAuthenticated = true;
            OnPlayerAdded?.Invoke(msgType.PlayFabId);
        }
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        var uconn = Connections.Find(c => c.ConnectionId == conn.connectionId);
        if (uconn == null)
        {
            Connections.Add(new UnityNetworkConnection()
            {
                Connection = conn,
                ConnectionId = conn.connectionId,
                LobbyId = PlayFabMultiplayerAgentAPI.SessionConfig.SessionId
            });
        }
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        var uconn = Connections.Find(c => c.ConnectionId == conn.connectionId);
        if (uconn != null)
        {
            if (!string.IsNullOrEmpty(uconn.PlayFabId))
            {
                OnPlayerRemoved?.Invoke(uconn.PlayFabId);
            }
            Connections.Remove(uconn);
        }
    }
}

