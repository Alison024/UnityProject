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
    [SerializeField]
    private GameObject weapon;
    //[SyncVar]
    [SerializeField]
    private GameObject weaponChild;


    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.Find("Weapon").gameObject;
        bulletSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.transform.childCount == 0)
        {
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

    public void PickUpWeapon(GameObject weaponPrefab)
    {
        weaponChild = Instantiate(weaponPrefab, weapon.transform.position, weapon.transform.rotation);//Quaternion.identity
        weaponChild.transform.parent = weapon.transform;
        bulletSpawnPosition = weaponChild.transform.GetChild(1).gameObject;
    }

}
