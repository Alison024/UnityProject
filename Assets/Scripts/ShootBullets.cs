using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootBullets : NetworkBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject bulletSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnPosition = transform.GetChild(1).gameObject;
        bulletSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            this.BulletShoot(bulletSpawnPosition.transform.right);
        }
    }

    [Command]
    void BulletShoot(Vector2 vector)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = vector * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
}
