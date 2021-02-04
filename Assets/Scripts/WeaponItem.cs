using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItem : NetworkBehaviour
{
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
