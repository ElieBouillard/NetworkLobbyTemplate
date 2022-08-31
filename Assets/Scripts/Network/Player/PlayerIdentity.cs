using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerIdentity : MonoBehaviour
{
    #region Fields
    private ushort _id;
    private bool _isLocalPlayer;
    #endregion
    
    #region Getters
    public ushort GetId() => _id;
    public bool IsLocalPlayer() => _isLocalPlayer;
    #endregion

    public virtual void Initialize(ushort id, string newName)
    {
        _id = id;

        if (_id == NetworkManager.Instance.GetClient().Id) { _isLocalPlayer = true; }

        gameObject.name = newName;
    }
}