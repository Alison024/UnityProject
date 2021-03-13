using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CustomNetworkManager : NetworkManager
{
    NetworkConnection conn;
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        //this.conn.
    }
}
