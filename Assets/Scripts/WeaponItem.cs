using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public GameObject weaponPrefab;
    private GameObject childrenWeapon;
    //private WeaponСharacteristics weaponСharacteristics;

    void Start()
    {
        childrenWeapon = Instantiate(weaponPrefab,new Vector3(0, 0, 1), Quaternion.identity);
        childrenWeapon.transform.parent = transform;
        //IWeapon weaponscript = (IWeapon)childrenWeapon.GetComponent(transform.name);
        //weaponСharacteristics = weaponscript.GetWeaponСharacteristics();
    }

    // Update is called once per frame
    /*void Update()
    {
        Debug.Log(weaponPrefab.name + "!!!!");
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player collision");
            childrenWeapon.transform.parent = collision.transform.Find("Weapon").transform;
            Destroy(transform.gameObject);
        }
        else
        {
            Debug.Log("Not Player collision");
        }
    }
    //private void CkeckChildComponents
}
