using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public GameObject weaponPrefab;
    [SerializeField]
    private GameObject childrenWeapon;

    void Start()
    {
        //v1
        //childrenWeapon = Instantiate(weaponPrefab,new Vector3(0, 0, 1), Quaternion.identity);
        //childrenWeapon.transform.parent = transform;
        //v2
        //childrenWeapon = Instantiate(weaponPrefab);
        //childrenWeapon.transform.parent = transform;
        //childrenWeapon.transform.position = new Vector3(0, 0, 1);
        //v1==v2
        //v3
        childrenWeapon = Instantiate(weaponPrefab,transform.position, Quaternion.identity);
        childrenWeapon.transform.parent = transform;
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            WeaponScript script = (WeaponScript)collision.GetComponent("WeaponScript");
            script.PickUpWeapon(weaponPrefab);
            Destroy(transform.gameObject);
        }
    }

 
}
