using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponСharacteristics
{
    
    private GameObject bulletPrefab;
    private float bulletSpeed;
    private float weight;
    private float fireRate;
    private float firingSpread;
    private int magazine;
    private float reloadTime;
    private EquippedWeapon equippedWeapon;
    public WeaponСharacteristics(GameObject BulletPrefab, 
        float Weight, float FireRate, float FiringSpread, float BulletSpeed, int Magazine, float ReloadTime, EquippedWeapon equippedWeapon)
    {
        this.bulletPrefab = BulletPrefab;
        this.weight = Weight;
        this.fireRate = FireRate;
        this.firingSpread = FiringSpread;
        this.bulletSpeed = BulletSpeed;
        this.magazine = Magazine;
        this.reloadTime = ReloadTime;
        this.equippedWeapon = equippedWeapon;
    }
    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
    public float GetWeight()
    {
        return weight;
    }
    public float GetFireRate()
    {
        return fireRate;
    }
    public float GetFiringSpread()
    {
        return firingSpread;
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
    public int GetMagazineAmo()
    {
        return magazine;
    }
    public float GetReloadSpeed()
    {
        return reloadTime;
    }
    public EquippedWeapon GetEquippedWeapon()
    {
        return equippedWeapon;
    }
}
