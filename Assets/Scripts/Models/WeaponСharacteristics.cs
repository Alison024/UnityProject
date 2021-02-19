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
    private EquippedWeapon equippedWeapon;
    public WeaponСharacteristics(GameObject BulletPrefab, 
        float Weight, float FireRate, float FiringSpread, float BulletSpeed, EquippedWeapon equippedWeapon)
    {
        this.bulletPrefab = BulletPrefab;
        this.weight = Weight;
        this.fireRate = FireRate;
        this.firingSpread = FiringSpread;
        this.bulletSpeed = BulletSpeed;
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
    public EquippedWeapon GetEquippedWeapon()
    {
        return equippedWeapon;
    }
}
