using RiptideNetworking;
using UnityEngine;

public class ServerMessages : MonoBehaviour
{
    internal enum MessagesId : ushort
    {
        PlayerConnectedToLobby = 1,
        PlayerDisconnectedFromLobby,
        StartGame,
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
    
    public void SendPlayerDisconnectedFromLobby(ushort playerId)
    {
        Message message = Message.Create(MessageSendMode.reliable, MessagesId.PlayerDisconnectedFromLobby);
        message.AddUShort(playerId);
        NetworkManager.Instance.GetServer().SendToAll(message, playerId);
    }

    private static void SendHostStartGame()
    {
        Message message = Message.Create(MessageSendMode.reliable, MessagesId.StartGame);
        NetworkManager.Instance.GetServer().SendToAll(message);
    }
    #endregion

    #region Received

    [MessageHandler((ushort) ClientMessages.MessagesId.StartGame)]
    private static void OnClientStartGame(ushort id, Message message)
    {
        if(id != 1) return;
        SendHostStartGame();
    }
    #endregion
}