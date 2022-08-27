using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class ServerMessages : MonoBehaviour
{
    internal enum MessagesId : ushort
    {
        PlayerConnectedToLobby = 1,
    }

    #region Send
    public void SendPlayerConnectedToLobby(ushort newPlayerId)
    {
        foreach (var player in NetworkManager.Instance.GetPlayers())
        {
            Message message1 = Message.Create(MessageSendMode.reliable, MessagesId.PlayerConnectedToLobby);
            message1.Add(player.Value.GetId());
            NetworkManager.Instance.GetServer().Send(message1, newPlayerId);
        }
        
        Message message2 = Message.Create(MessageSendMode.reliable, MessagesId.PlayerConnectedToLobby);
        message2.Add(newPlayerId);
        NetworkManager.Instance.GetServer().SendToAll(message2);
    }

    #endregion

    #region Received

    

    #endregion
}

public enum ServerMessageId
{
    
}