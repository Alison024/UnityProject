using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public EquippedWeapon equippedWeapon;
    public GameObject weaponPrefab;
    private GameObject childrenWeapon;

    void Start()
    {
        childrenWeapon = Instantiate(weaponPrefab,transform.position, Quaternion.identity);
        childrenWeapon.transform.parent = transform;
        equippedWeapon = EquippedWeapon.ak47;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.gameObject.GetComponent<WeaponScript>().CmdPickUpWeapon(equippedWeapon);
            //Invoke("Destroy",0.2f);
            Destroy(transform.gameObject);
        }
    }
    private void Destroy()
    {
        Destroy(transform.gameObject);
    }
 
}
