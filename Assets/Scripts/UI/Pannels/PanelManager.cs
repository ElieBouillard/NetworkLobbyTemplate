using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] private Panel[] _panels;

    protected override void Awake()
    {
        base.Awake();

        EnablePanel(PanelType.MainMenu);
    }

    public void EnablePanel(PanelType panelType)
    {
        foreach (var panel in _panels)
        {
            panel.gameObject.SetActive(panel.PanelType == panelType);
        }
    }
}

public enum PanelType
{
    MainMenu = 1,
    Options,
    Lobby,
}