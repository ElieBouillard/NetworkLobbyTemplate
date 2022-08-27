using System;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    #region UnityInspector
    [Header("NetworkSettings")] [Space(20)] 
    [SerializeField] private bool _useSteam;
    [SerializeField] private ushort _port = 7777;
    [SerializeField] private ushort _maxPlayer = 4;
    
    [Header("PlayerObjects")]
    [SerializeField] private GameObject _lobbyPlayerPrefab;
    [SerializeField] private GameObject _localPlayerPrefab;
    [SerializeField] private GameObject _generalPlayerPrefab;
    #endregion
    
    #region Fields
    private Server _server;
    private Client _client; 
    /*Set To Private */ public GameState _gameState = GameState.OffLine;
    private Dictionary<ushort, PlayerIdentity> _players = new Dictionary<ushort, PlayerIdentity>();
    private ClientMessages _clientMessages;
    private ServerMessages _serverMessages;
    #endregion

    #region Getters
    public bool GetUseSteam() => _useSteam;
    public Server GetServer() => _server;
    public Client GetClient() => _client;
    public GameState GetGameState() => _gameState;
    public Dictionary<ushort, PlayerIdentity> GetPlayers() => _players;
    public ClientMessages GetClientMessages() => _clientMessages;
    public ServerMessages GetServerMessages() => _serverMessages;
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        _clientMessages = gameObject.AddComponent<ClientMessages>();
        _serverMessages = gameObject.AddComponent<ServerMessages>();
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        
        InitializeClient();
        InitializeServer();
    }

    private void FixedUpdate()
    {
        _client.Tick();
        
        if(_server.IsRunning) _server.Tick();
    }

    private void OnApplicationQuit()
    {
        switch (_gameState)
        {
            case GameState.OffLine:
                break;
            case GameState.Lobby:
                LeaveLobby();
                break;
            case GameState.Gameplay:
                break;
        }
    }

    private void InitializeClient()
    {
        _client = new Client();

        _client.Connected += ClientOnConnected;
        _client.Disconnected += ClientOnDisconnected;
        _client.ConnectionFailed += ClientOnConnectionFailed;
        _client.ClientConnected += ClientOnPlayerJoin;
        _client.ClientDisconnected += ClientOnPlayerLeft;
    }

    private void InitializeServer()
    {
        _server = new Server();

        _server.ClientConnected += ServerOnClientConnected;
        _server.ClientDisconnected += ServerOnClientDiconnected;
    }
    
    #region ClientCallbacks
    private void ClientOnConnected(object sender, EventArgs e)
    {
        _gameState = GameState.Lobby;
        
        PanelManager.Instance.EnablePanel(PanelType.Lobby);
    }

    private void ClientOnDisconnected(object sender, EventArgs e)
    {
        _gameState = GameState.OffLine;
        
        PanelManager.Instance.EnablePanel(PanelType.MainMenu);
    }
    
    private void ClientOnConnectionFailed(object sender, EventArgs e)
    {
        
    }
    
    private void ClientOnPlayerJoin(object sender, ClientConnectedEventArgs e)
    {
        
    }
    
    private void ClientOnPlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        
    }
    #endregion

    #region ServerCallbacks
    private void ServerOnClientConnected(object sender, ServerClientConnectedEventArgs e)
    {
        switch (_gameState)
        {
            case GameState.OffLine:
                break;
            case GameState.Lobby:
                _serverMessages.SendPlayerConnectedToLobby(e.Client.Id);
                break;
            case GameState.Gameplay:
                _server.DisconnectClient(e.Client.Id);
                break;
        }
    }
    
    private void ServerOnClientDiconnected(object sender, ClientDisconnectedEventArgs e)
    {
        
    }
    #endregion

    #region ClientFunctions
    public void StartHost()
    {
        _server.Start(_port, _maxPlayer);
        _client.Connect($"127.0.0.1:{_port}");
    }
    
    public void JoinLobby()
    {
        _client.Connect($"127.0.0.1:{_port}");
    }
    
    public void LeaveLobby()
    {
        _client.Disconnect();
        ClientOnDisconnected(new object(), EventArgs.Empty);
        
        _server.Stop();
    }
    #endregion

    #region ServerOnClientFunctions

    public void AddPlayerToLobby(ushort newPlayerId)
    {
        GameObject playerInstance = Instantiate(_lobbyPlayerPrefab, SpawnPointsManager.Instance.SpawnPointsCoord[_players.Count], Quaternion.identity);
        PlayerIdentity playerIdentityInstance = playerInstance.GetComponent<PlayerIdentity>();
        playerIdentityInstance.Initialize(newPlayerId);
        
        _players.Add(newPlayerId, playerIdentityInstance);
    }
    #endregion
}

public enum GameState
{
    OffLine = 1,
    Lobby,
    Gameplay,
}