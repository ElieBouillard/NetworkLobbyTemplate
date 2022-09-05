using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : Panel
{
    [Header("References")] 
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _leaveLobbyButton;
    
    protected override void AssignButtonsReference()
    {
        _startGameButton.onClick.AddListener(OnClickStartGame);
        _leaveLobbyButton.onClick.AddListener(OnClickLeaveLobby);
    }

    public void EnableStartButton(bool value)
    {
        _startGameButton.gameObject.SetActive(value);
    }
    
    private void OnClickStartGame()
    {
        NetworkManager.Instance.StartGame();
    }

    private void OnClickLeaveLobby()
    {
        NetworkManager.Instance.LeaveLobby();
    }
}