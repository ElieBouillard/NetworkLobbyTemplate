using RiptideNetworking;
using UnityEngine;

public class ClientMessages : MonoBehaviour
{
    internal enum MessagesId : ushort
    {
    
    }
    
    #region Send

    

    #endregion

    #region Received
    [MessageHandler((ushort) ServerMessages.MessagesId.PlayerConnectedToLobby)]
    private static void OnClientConnectedToLobby(Message message)
    {
        LobbyManager.Instance.AddPlayerToLobby(message.GetUShort());
    } 
    
    [MessageHandler((ushort) ServerMessages.MessagesId.PlayerDisconnectedFromLobby)]
    private static void OnClientDisconnectedFromLobby(Message message)
    {
        LobbyManager.Instance.RemovePlayerFromLobby(message.GetUShort());
    } 
    #endregion
}
