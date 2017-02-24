using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ChatBox : NetworkBehaviour
{

    private void SendChat(string message)
    {
        if (isServer)
        {
            Rpc_ChatMessage(message);
        }
        else
        {
            Cmd_ChatMessage(message);
        }
    }

    [Command]
    private void Cmd_ChatMessage(string message)
    {
        Rpc_ChatMessage(message);
    }
    [ClientRpc]
    private void Rpc_ChatMessage(string message)
    {
        Debug.Log(message);
    }
}
