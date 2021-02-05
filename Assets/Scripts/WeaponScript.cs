using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponScript : NetworkBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject bulletSpawnPosition;

    private GameObject weapon;


    // Start is called before the first frame update
    void Start()
    {
        /*if (transform.Find("Weapon").childCount != 0)
        {
            bulletSpawnPosition = transform.Find("Weapon").GetChild(1).gameObject;
        }
        bulletSpeed = 15;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.Find("Weapon").childCount == 0)
        {
            return;
        }

        /*if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            this.CmdBulletShoot(bulletSpawnPosition.transform.right);
        }*/
    }

    [Command]
    void CmdBulletShoot(Vector2 vector)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = vector * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
    /*private void CheckBulletSpawnPosition()
    {
        bulletSpawnPosition = transform.Find("Weapon").GetChild(1).gameObject;
    }
    private void CheckWeaponCharacteristic()
    {
        //IWeapon weapon = 
    }*/
    public void PickUpWeapon()
    {
        weapon = transform.Find("Weapon").GetChild(0).gameObject;
        
    }

}
