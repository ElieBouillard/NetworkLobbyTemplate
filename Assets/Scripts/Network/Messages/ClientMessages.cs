using RiptideNetworking;
using UnityEngine;

public class ClientMessages : MonoBehaviour
{
    internal enum MessagesId : ushort
    {
        StartGame = 1,
    }
    
    #region Send
    public void SendStartGame()
    {
        Message message = Message.Create(MessageSendMode.reliable, MessagesId.StartGame);
        NetworkManager.Instance.GetClient().Send(message);
    }
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
    
    [MessageHandler((ushort) ServerMessages.MessagesId.StartGame)]
    private static void OnServerStartGame(Message message)
    {
        NetworkManager.Instance.OnServerStartGame();
    } 
    #endregion
}
