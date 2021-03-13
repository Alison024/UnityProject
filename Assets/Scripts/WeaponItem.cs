using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public WeaponItemSpawner weaponItemSpawner;
    public EquippedWeapon equippedWeapon;
    public GameObject weaponPrefab;
    private GameObject childrenWeapon;

    void Start()
    {
        childrenWeapon = Instantiate(weaponPrefab,transform.position, Quaternion.identity);
        childrenWeapon.transform.parent = transform;
        equippedWeapon = EquippedWeapon.ak47;
        GetComponent<BoxCollider2D>().size = childrenWeapon.transform.GetChild(0).GetComponent<SpriteRenderer>().size;
        GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, -0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Destroy(transform.gameObject);
        }
    }
    private void Destroy()
    { 
        weaponItemSpawner.weaponItems--;
        Destroy(transform.gameObject);
        
    }
 
}
