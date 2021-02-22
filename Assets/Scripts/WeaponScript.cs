using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum EquippedWeapon : int
{
    nothing,
    ak47
}
public class WeaponScript : NetworkBehaviour
{

    public GameObject bulletPrefab;
    private float bulletSpeed;
    private GameObject bulletSpawnPosition;
    private GameObject weaponParent;
    public GameObject equippedWeaponPrefab;
    private GameObject equippedWeaponGo;//Go = gameobject
    [SyncVar(hook = nameof(OnChangeWeapon))]
    private EquippedWeapon equippedWeapon = EquippedWeapon.nothing;
    void Start()
    {
        weaponParent = transform.Find("Weapon").gameObject;
        bulletSpeed = 30;
    }
    void Update()
    {
        /*if (bulletSpawnPosition != null)
        {
            Vector3 positionOnScreenA = bulletSpawnPosition.transform.position;
            Vector3 mouseOnScreenB = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //Vector3 tmp = mouseOnScreenB - positionOnScreenA;
            Debug.DrawRay(positionOnScreenA, mouseOnScreenB-positionOnScreenA, Color.red);
        }*/
        if (weaponParent.transform.childCount == 0)
        {
            return;
        }

        if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            this.CmdBulletShoot();
        }
    }
    private void OnChangeWeapon(EquippedWeapon oldEquippedWeapon, EquippedWeapon newEquippedWeapon)
    {
        if(newEquippedWeapon == EquippedWeapon.ak47)
        {
            
            equippedWeaponGo = Instantiate(equippedWeaponPrefab, weaponParent.transform.position, weaponParent.transform.rotation);
            equippedWeaponGo.transform.parent = weaponParent.transform;
            bulletSpawnPosition = equippedWeaponGo.transform.GetChild(1).gameObject;
        }
    }
    [Command]
    void CmdBulletShoot()
    {
        /*Vector3 positionOnScreenA = Camera.main.WorldToViewportPoint(bulletSpawnPosition.transform.position);
        Vector3 mouseOnScreenB = (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition);*/
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPosition.transform.right * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
        
    }
    [Command]
    public void PickUpWeapon(EquippedWeapon equipped)
    {
        equippedWeapon = equipped;
    }
    
}
