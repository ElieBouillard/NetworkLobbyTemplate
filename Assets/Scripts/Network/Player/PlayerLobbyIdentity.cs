using Steamworks;
using TMPro;
using UnityEngine;

public class PlayerLobbyIdentity : PlayerIdentity
{
    [SerializeField] private TMP_Text _playerPseudoText;
    [SerializeField] private GameObject _localSprite;

    public override void Initialize(ushort id, string newName)
    {
        base.Initialize(id, newName);

        _playerPseudoText.text = newName;
        
        NetworkManager networkManager = NetworkManager.Instance;
        
        if (id == networkManager.GetClient().Id) LobbyManager.Instance.LobbyPanel.EnableStartButton(id == 1);
        
        _localSprite.SetActive(id == networkManager.GetClient().Id);
    }

    public void Initialize(ushort id, ulong steamId)
    {
        Initialize(id, SteamFriends.GetFriendPersonaName((CSteamID)steamId));
        
        _steamId = steamId;
    }
}
