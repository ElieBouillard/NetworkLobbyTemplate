using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _lobbyPlayerPrefab;
    [SerializeField] private GameObject _startButton;

    private void Start()
    {
        _startButton.GetComponent<Button>().onClick.AddListener(NetworkManager.Instance.StartGame);
    }

    public void AddPlayerToLobby(ushort newPlayerId)
    {
        NetworkManager networkManager = NetworkManager.Instance;
        
        GameObject playerInstance = Instantiate(_lobbyPlayerPrefab, _spawnPoints[networkManager.GetPlayers().Count].position, Quaternion.identity);
        PlayerLobbyIdentity playerLobbyIdentityInstance = playerInstance.GetComponent<PlayerLobbyIdentity>();
        
        playerLobbyIdentityInstance.Initialize(newPlayerId, GetPlayerName(networkManager.GetClient().Id, newPlayerId));

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

    public void SetStartGameButton(bool value)
    {
        _startButton.SetActive(value);
    }
}