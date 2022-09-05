using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    public LobbyPanel LobbyPanel;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _lobbyPlayerPrefab;

    protected override void Awake()
    {
        base.Awake();

        if (NetworkManager.Instance.GetUseSteam())
        {
            gameObject.AddComponent<SteamLobbyManager>();
        }
    }

    public void AddPlayerToLobby(ushort newPlayerId, ulong steamId)
    {
        NetworkManager networkManager = NetworkManager.Instance;
        
        GameObject playerInstance = Instantiate(_lobbyPlayerPrefab, _spawnPoints[networkManager.GetPlayers().Count].position, Quaternion.identity);
        PlayerLobbyIdentity playerLobbyIdentityInstance = playerInstance.GetComponent<PlayerLobbyIdentity>();
        
        if(!networkManager.GetUseSteam()) playerLobbyIdentityInstance.Initialize(newPlayerId, GetPlayerName(networkManager.GetClient().Id, newPlayerId));
        else playerLobbyIdentityInstance.Initialize(newPlayerId, steamId);
        
        if (newPlayerId == networkManager.GetClient().Id){ networkManager.SetLocalPlayer(playerLobbyIdentityInstance); }

        networkManager.GetPlayers().Add(newPlayerId, playerLobbyIdentityInstance);
    }

    public void RemovePlayerFromLobby(ushort playerId)
    {
        NetworkManager networkManager = NetworkManager.Instance;

        foreach (var player in networkManager.GetPlayers())
        {
            if (player.Key == playerId)
            {
                Destroy(player.Value.gameObject);
                networkManager.GetPlayers().Remove(player.Key);
                ReorganizeLobbyPosition();
                return;
            }
        }
    }

    private void ReorganizeLobbyPosition()
    {
        NetworkManager networkManager = NetworkManager.Instance;
        int posIndex = 0;
        foreach (var player in networkManager.GetPlayers())
        {
            player.Value.gameObject.transform.position = _spawnPoints[posIndex].position;
            posIndex++;
        }
    }
    
    public void ClearLobby()
    {
        NetworkManager networkManager = NetworkManager.Instance;
        
        foreach (var player in networkManager.GetPlayers())
        {
            Destroy(player.Value.gameObject);
        }
            
        networkManager.GetPlayers().Clear();
    }
    
    private string GetPlayerName(ushort clientId, ushort playerId)
    {
        if (playerId == 1) return $"Host : {playerId}";

        string statueName = playerId == clientId ? "Local" : "Client";
        
        return $"{statueName} : {playerId}";
    }
}