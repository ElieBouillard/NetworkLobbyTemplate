using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [Header("References")]
    [SerializeField] private Button _createGameButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;

    protected override void AssignButtonsReference()
    {
        _createGameButton.onClick.AddListener(OnClickCreateGame);
        _optionsButton.onClick.AddListener(OnClickOptions);
        _quitButton.onClick.AddListener(OnClickQuit);
    }

    private void OnClickCreateGame()
    {
        NetworkManager.Instance.StartHost();
    }

    private void OnClickOptions()
    {
        PanelManager.Instance.EnablePanel(PanelType.Options);
    }

    private void OnClickQuit()
    {
        Application.Quit();
    }
}