using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class ClientMessages : MonoBehaviour
{
    #region Send

    

    #endregion

    #region Received

    [MessageHandler((ushort) ServerMessages.MessagesId.PlayerConnectedToLobby)]
    private static void OnClientConnectedToLobby(Message message)
    {
        NetworkManager.Instance.AddPlayerToLobby(message.GetUShort());
    }
    

    #endregion
    
    public enum ClientMessageId
    {
    
    }
}
