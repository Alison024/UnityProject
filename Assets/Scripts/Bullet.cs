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
        if (damage == null)
        {
            damage = 10;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            NetworkServer.Destroy(gameObject);
        }
        //попытка исправить баг с отображением пуль на стороне клиента
        /*if(collision.tag == "Player")
        {
            if(collision.GetComponent<NetworkIdentity>().netId == playerId)
            {
                Physics2D.IgnoreCollision(collision.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
            }
        }*/
        //не получилось
    }
}
