using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    private GameObject bulletStartPosition;
    private const float WEIGHT = 2;
    private const float FIRERATE = 600;//600 выстрелов в минуту
    private const float FIRINGSPREAD = 10;//10  градусов
    private const float BULLETSPEED = 15;
    void Start()
    {
        bulletStartPosition = transform.GetChild(1).gameObject;
    }

    public WeaponСharacteristics GetWeaponСharacteristics()
    {
        return new WeaponСharacteristics(bulletStartPosition,bulletPrefab,WEIGHT,FIRERATE,FIRINGSPREAD, BULLETSPEED);
    }
}
