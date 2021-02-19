using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class WeaponScript : NetworkBehaviour
{

    private GameObject bulletPrefab;
    private float bulletSpeed;
    private GameObject bulletSpawnPosition;
    private GameObject weapon;
    private GameObject weaponChild;

    [SyncVar(hook = nameof(SyncWeapon))]
    private GameObject equippedWeapon;
    void Start()
    {
        weapon = transform.Find("Weapon").gameObject;
        bulletSpeed = 15;
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        equippedWeapon = new GameObject("Empty"); ;
    }
    private void SyncWeapon(GameObject oldWeapon, GameObject newWeapon)
    {
        if (newWeapon == null)
        {
            Debug.Log("HHHHOOOOOOKKKKKKK_NULL_!!!!!!!!!!");
            return;
        }
        Debug.Log("HHHHOOOOOOKKKKKKK!!!!!!!!!!");
        InitWeapon();
    }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WeaponItem")
        {
            try
            {


                WeaponItem wi = (WeaponItem)collision.GetComponent("WeaponItem");
                if (wi.weaponPrefab == null)
                {
                    Debug.Log("FAKE_PICKUP!!!!!!!!!!!!!!!!!!!!");
                }
                else
                {
                    Debug.Log("PICKUP_NOT_FAKE = " + wi.weaponPrefab.ToString());
                }
                equippedWeapon = wi.weaponPrefab;
                if (isServer)
                {
                    InitWeapon();
                    Debug.Log("PICKUP_ON_SERVER ");
                }
                Debug.Log("PICKUP_EQUIPED_WEAPON - " + equippedWeapon.ToString());
            }catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }
            //CmdPickUpWeapon(wi.weaponPrefab);
            //Debug.Log("PICKUP!!!!!!!!!!!!!!!!!!!!");
            //Destroy(collision.gameObject);
        }
    }
    /*[Command]
    public void CmdPickUpWeapon(GameObject weaponPrefab)
    {
        Debug.Log("PICKUP_ON_SERVER!!!!!!!!!!!!!!!!!!!!"+ weaponPrefab.ToString());
        RpcLogFromServer();
        equippedWeapon = weaponPrefab;
        
    }
    [ClientRpc]
    public void RpcLogFromServer()
    {
        Debug.Log("PICKUP_FROM_SERVER!!!!!!!!!!!!!!!!!!!!");
    }*/
    private void InitWeapon()
    {
        weaponChild = Instantiate(equippedWeapon, weapon.transform.position, weapon.transform.rotation);//Quaternion.identity
        weaponChild.transform.parent = weapon.transform;
        bulletSpawnPosition = weaponChild.transform.GetChild(1).gameObject;
    }

}
