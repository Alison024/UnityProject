using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public GameObject weaponPrefab;
    private GameObject childrenWeapon;

    void Start()
    {
        childrenWeapon = Instantiate(weaponPrefab,transform.position, Quaternion.identity);
        childrenWeapon.transform.parent = transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            collision.gameObject.GetComponent<WeaponScript>().PickUpWeapon(EquippedWeapon.ak47);
            Destroy(transform.gameObject);
        }
    }

 
}
