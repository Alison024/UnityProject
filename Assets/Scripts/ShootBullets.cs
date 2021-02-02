using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootBullets : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public GameObject bulletSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        /*Transform transform = this.transform.Find("BulletStartPosition");
        if (transform != null)
        {
            bulletSpawnPosition = transform.gameObject;
        }
        else
        {
            bulletSpawnPosition = null;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            
            this.BulletShoot(new Vector2(bulletSpawnPosition.transform.position.x, 0));
        }
    }
    [Command]
    void BulletShoot(Vector2 vector)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        Debug.Log("Shoot");
        bullet.GetComponent<Rigidbody2D>().velocity = vector * bulletSpeed;
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 1.0f);

    }
}
