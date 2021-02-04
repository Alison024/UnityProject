using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponСharacteristics
{
    private GameObject bulletSpawnPositon;
    private GameObject bulletPrefab;
    private float bulletSpeed;
    private float weight;
    private float fireRate;
    private float firingSpread;

    public WeaponСharacteristics(GameObject BulletSpawnPostion, GameObject BulletPrefab, 
        float Weight, float FireRate, float FiringSpread, float BulletSpeed)
    {
        this.bulletSpawnPositon = BulletSpawnPostion;
        this.bulletPrefab = BulletPrefab;
        this.weight = Weight;
        this.fireRate = FireRate;
        this.firingSpread = FiringSpread;
        this.bulletSpeed = BulletSpeed;
    }
    public GameObject GetBulletSpawnPostion()
    {
        return bulletSpawnPositon;
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
}
