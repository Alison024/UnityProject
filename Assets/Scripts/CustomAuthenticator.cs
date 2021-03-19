using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Authenticators;
public class CustomAuthenticator : NetworkAuthenticator
{
    public int PlayerId { get; set; }
    public string PlayerNickName { get; set; }
    public string PlayerLogin { get; set; }

    public struct AuthRequestMessage : NetworkMessage
    {
        public string authUsername;
        //public string authPassword;
    }

    public struct AuthResponseMessage : NetworkMessage
    {
        public byte code;
        public string message;
    }
    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
    }
    public override void OnStopServer()
    {
        NetworkServer.UnregisterHandler<AuthRequestMessage>();
    }

    public override void OnServerAuthenticate(NetworkConnection conn){}
    public void OnAuthRequestMessage(NetworkConnection conn, AuthRequestMessage msg)
    {
        if (true)//PlayerId!=0 && PlayerNickName!=null && PlayerLogin!=null
        {
            AuthResponseMessage authResponseMessage = new AuthResponseMessage
            {
                code = 100,
                message = "Success"
            };

            conn.Send(authResponseMessage);
            PlayerData playerData = new PlayerData { 
                PlayerId = PlayerId, 
                PlayerLogin = PlayerLogin, 
                PlayerNickName = PlayerNickName
            };

            conn.authenticationData = playerData;
            // Accept the successful authentication
            ServerAccept(conn);
        }
        else
        {
            AuthResponseMessage authResponseMessage = new AuthResponseMessage
            {
                code = 200,
                message = "Invalid Credentials"
            };
            conn.Send(authResponseMessage);
            // must set NetworkConnection isAuthenticated = false
            conn.isAuthenticated = false;
            // disconnect the client after 1 second so that response message gets delivered
            StartCoroutine(DelayedDisconnect(conn, 1));
        }
    }
    
    
    IEnumerator DelayedDisconnect(NetworkConnection conn, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ServerReject(conn);
    }


    public override void OnStartClient()
    {
        NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
    }

    public override void OnStopClient()
    {
        NetworkClient.UnregisterHandler<AuthResponseMessage>();
    }

    public override void OnClientAuthenticate(NetworkConnection conn)
    {
        AuthRequestMessage authRequestMessage = new AuthRequestMessage
        {
            authUsername = PlayerLogin
        };
        conn.Send(authRequestMessage);
    }


    public void OnAuthResponseMessage(NetworkConnection conn, AuthResponseMessage msg)
    {
        if (msg.code == 100)
        {
            ClientAccept(conn);
        }
        else
        {
            Debug.LogError($"Authentication Response: {msg.message}");
            ClientReject(conn);
        }
    }
}
