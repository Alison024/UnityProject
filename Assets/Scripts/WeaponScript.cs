using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum EquippedWeapon : byte
{
    nothing,
    ak47
}
public class WeaponScript : NetworkBehaviour
{
    
    public GameObject bulletPrefab;
    public GameObject equippedWeaponPrefab;

    private float bulletSpeed;
    private float fireRate;
    private float lastFire;
    private float firingSpread;
    private int magazine;
    private int currentMagazine;
    private float reloadTime;
    private bool isReloading;

    private GameObject bulletSpawnPosition;
    private GameObject weaponParent;
    private SpriteRenderer weaponSpriteRenderer;
    private GameObject equippedWeaponGo;

    [SyncVar(hook = nameof(OnChangeWeapon))]
    private EquippedWeapon equippedWeapon = EquippedWeapon.nothing;
    private bool isFlipWeaponY = false;

    void Start()
    {
        bulletSpeed = 15;
        fireRate = 600;
        firingSpread = 10;
        magazine = 20;
        reloadTime = 2;
        isReloading = false;
        lastFire = 0;
        currentMagazine = magazine;
        weaponParent = transform.Find("WeaponRight").gameObject;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (weaponParent.transform.childCount != 0)
        {
            Fire();
            RotateWeapon();
        }
        
    }
    void OnChangeWeapon(EquippedWeapon oldEquippedWeapon, EquippedWeapon newEquippedWeapon)
    {
        if (newEquippedWeapon == EquippedWeapon.ak47 && weaponParent!=null)
        {
            Vector3 weaponPos = weaponParent.transform.position;
            equippedWeaponGo = Instantiate(equippedWeaponPrefab, 
                new Vector3(weaponParent.transform.position.x + 0.9f, weaponParent.transform.position.y, weaponParent.transform.position.z),
                weaponParent.transform.rotation);
            equippedWeaponGo.transform.parent = weaponParent.transform;
            weaponSpriteRenderer = equippedWeaponGo.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            bulletSpawnPosition = equippedWeaponGo.transform.GetChild(1).gameObject;
        }
    }
   
    [Command]
    void CmdFire()
    {
        CreateBullet();
    }
    
    private void CreateBullet()
    {
        try{
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().playerId = netId;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPosition.transform.right * bulletSpeed;
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 5.0f);
        }catch(Exception ex){
            if(bulletPrefab == null)
            {
                Debug.Log("Bullet prefab = null");
                Debug.Log("Bullet: " + ex.Message);
            }
            else if (bulletSpawnPosition == null)
            {
                Debug.Log("Bullet Spawn Position = null");
                Debug.Log("Bullet: " + ex.Message);
            }
            else Debug.Log("Bullet: "+ex.Message);
        }
    }

    [Command]
    public void CmdPickUpWeapon(EquippedWeapon equipped)
    {
        equippedWeapon = equipped;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WeaponItem")//isLocalPlayer
        {
            EquippedWeapon equippedWeaponValue = collision.GetComponent<WeaponItem>().equippedWeapon;
            CmdPickUpWeapon(equippedWeaponValue);
        }
    }
    private void RotateWeapon()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector2 positionOnScreen = weaponParent.transform.position;
        Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        weaponParent.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        
        if ((positionOnScreen - mouseOnScreen).x < 0)
        {
            weaponSpriteRenderer.flipY = false;
        }
        else
        {
            weaponSpriteRenderer.flipY = true;
        }
    }
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
    
    private void StartReloading()
    {
        isReloading = true;
        Invoke("EndReloading", reloadTime);
    }
    private void EndReloading()
    {
        isReloading = false;
        currentMagazine = magazine;
    }
    private void Fire()
    {
        if (Input.GetMouseButton(0) && !isReloading)
        {
            if (currentMagazine == 0)
            {
                StartReloading();
            }
            else if ((Time.time - lastFire) > (60 / fireRate))
            {
                lastFire = Time.time;
                CmdFire();
                currentMagazine--;
            }
        }
    }
}
