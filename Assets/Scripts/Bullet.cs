using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Bullet : NetworkBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public uint playerId;
    private void Start()
    {
        /*if (damage == null)
        {
            damage = 10;
        }*/
        damage = 10;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            NetworkServer.Destroy(gameObject);
        }
        
    }
}
