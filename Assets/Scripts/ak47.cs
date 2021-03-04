using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class AK47 : MonoBehaviour, IWeapon
{
    
    public GameObject bulletPrefab;
    private const float WEIGHT = 2;
    private const float FIRERATE = 600;//600 выстрелов в минуту
    private const float FIRINGSPREAD = 10;//10  градусов
    private const float BULLETSPEED = 15;
    private const int MAGAZINE = 20;
    private const float RELOADTIME = 2;
    private const EquippedWeapon EQUIPPEDWEAPON = EquippedWeapon.ak47;
    public WeaponСharacteristics GetWeaponСharacteristics()
    {
        return new WeaponСharacteristics(bulletPrefab,WEIGHT,FIRERATE,FIRINGSPREAD, BULLETSPEED,MAGAZINE, RELOADTIME,EQUIPPEDWEAPON);
    }
}
