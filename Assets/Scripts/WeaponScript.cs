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
    public Camera camera;

    private float bulletSpeed;
    private GameObject bulletSpawnPositionRight;
    private GameObject weaponParentRight;
    public GameObject equippedWeaponPrefab;
    private SpriteRenderer weaponSpriteRenderer;
    private GameObject equippedWeaponRightGo;//Go = gameobject
    [SyncVar(hook = nameof(OnChangeWeapon))]
    private EquippedWeapon equippedWeapon = EquippedWeapon.nothing;
    //[SyncVar(hook = nameof(OnFlipWeapon))]
    private bool isFlipWeaponY = false;
    void Start()
    {
        weaponParentRight = transform.Find("WeaponRight").gameObject;
        //camera = GetComponent<Camera>();
        bulletSpeed = 10;
    }
    void Update()
    { 
        
        if (weaponParentRight.transform.childCount == 0)
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
        Vector3 weaponPos = weaponParentRight.transform.position;
        if (newEquippedWeapon == EquippedWeapon.ak47)
        {
            equippedWeaponRightGo = Instantiate(equippedWeaponPrefab, new Vector3(weaponPos.x + 0.9f, weaponPos.y, weaponPos.z), weaponParentRight.transform.rotation);
            equippedWeaponRightGo.transform.parent = weaponParentRight.transform;
            weaponSpriteRenderer = equippedWeaponRightGo.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            bulletSpawnPositionRight = equippedWeaponRightGo.transform.GetChild(1).gameObject;

        }
    }
    /*private void OnFlipWeapon(bool oldVal, bool newVal)
    {
        if (weaponSpriteRenderer != null)
        {
            weaponSpriteRenderer.flipY = isFlipWeaponY;
        }
    }*/
    [Command]
    void CmdBulletShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPositionRight.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().playerId = netId;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPositionRight.transform.right * bulletSpeed;
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
        Vector2 positionOnScreen = weaponParentRight.transform.position;
        Vector2 mouseOnScreen = camera.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 lookDir = mouseOnScreen - positionOnScreen;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;//v1
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        weaponParentRight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        
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
    
}
