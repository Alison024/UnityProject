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
        if (transform.Find("Weapon").childCount != 0)
        {
            bulletSpawnPosition = transform.GetChild(1).gameObject;
        }
        bulletSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.Find("Weapon").childCount == 0)
        {
            Debug.Log("Child Count = " + transform.Find("Weapon").childCount);
            return;
        }

        if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            this.CmdBulletShoot(bulletSpawnPosition.transform.right);
        }
    }

    [Command]
    void CmdBulletShoot(Vector2 vector)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = vector * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
    
}
