using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    #region Fields
    private ushort _id;
    private ulong _steamId;
    private string _steamName;
    private bool _isLocalPlayer;
    #endregion
    
    #region Getters
    public ushort GetId() => _id;
    public ulong GetSteamId() => _steamId;
    public string GetSteamName() => _steamName;
    public bool IsLocalPlayer() => _isLocalPlayer;
    #endregion

    public void Initialize(ushort id)
    {
        _id = id;
    }
}
