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

    public GameObject bulletImgPrefab;
    private GameObject ammoPanel;

    [SyncVar(hook = nameof(OnChangeWeapon))]
    private EquippedWeapon equippedWeapon = EquippedWeapon.nothing;
    private bool isFlipWeaponY = false;

    private AudioSource shoot;
    private AudioSource reload;
    void Start()
    {
        bulletSpeed = 15;
        fireRate = 600;
        firingSpread = 10;
        magazine = 20;
        reloadTime = 2;
        isReloading = false;
        lastFire = 0;
        currentMagazine = 0;
        shoot = transform.Find("ShootAudioSourse").GetComponent<AudioSource>();
        reload = transform.Find("ReloadAudioSource").GetComponent<AudioSource>();
        weaponParent = transform.Find("WeaponRight").gameObject;
        if (isLocalPlayer)
        {
            ammoPanel = GameObject.Find("AmmoPanel");
        }
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
            currentMagazine = magazine;
            if (isLocalPlayer)
            {
                ReloadAmmmoUI();
            }
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
            NetworkServer.Spawn(bullet,connectionToClient);
            Destroy(bullet, 5.0f);
        }catch(Exception ex){
            Debug.Log("Bullet: "+ex.Message);
        }
    }

    [Command]
    public void CmdPickUpWeapon(EquippedWeapon equipped)
    {
        equippedWeapon = equipped;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WeaponItem")
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
        reload.PlayOneShot(reload.clip);
        isReloading = true;
        Invoke("EndReloading", reloadTime);
    }
    private void EndReloading()
    {
        reload.Stop();
        isReloading = false;
        currentMagazine = magazine;
        ReloadAmmmoUI();
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
                shoot.PlayOneShot(shoot.clip);
                currentMagazine--;
                DecrementAmmoUI();
            }
        }
    }
    private void ReloadAmmmoUI()
    {
        if (!isLocalPlayer)
            return;
        for(int i = 0; i < magazine; i++)
        {
            Instantiate(bulletImgPrefab, ammoPanel.transform);
        }
    }
    private void DecrementAmmoUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (ammoPanel.transform.childCount > 0)
        {
            Destroy(ammoPanel.transform.GetChild(0).gameObject);
            
        }
    }
}
