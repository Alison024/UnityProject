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
        bulletSpeed = 10;
    }
    void Update()
    { 
        
        if (weaponParent.transform.childCount == 0)
        {
            return;
        }
        if (this.isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            this.CmdBulletShoot();
        }
        RotateWeapon();
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
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().playerId = netId;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPosition.transform.right * bulletSpeed;
        //Ignore collisions with player-creator ... still doesn't work 
        //Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 5.0f);
    }
    [Command]
    public void PickUpWeapon(EquippedWeapon equipped)
    {
        equippedWeapon = equipped;
    }
    private void RotateWeapon()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(weaponParent.transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        weaponParent.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}
