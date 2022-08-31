using TMPro;
using UnityEngine;

public class PlayerLobbyIdentity : PlayerIdentity
{
    [SerializeField] private TMP_Text _playerPseudoText;

    public override void Initialize(ushort id, string newName)
    {
        base.Initialize(id, newName);

        _playerPseudoText.text = newName;
        
        if(id == NetworkManager.Instance.GetClient().Id) LobbyManager.Instance.SetStartGameButton(id == 1); 
    }
}
