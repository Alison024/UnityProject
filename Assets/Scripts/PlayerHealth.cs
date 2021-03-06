﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerHealth : NetworkBehaviour
{
    public GameObject healthBar;
    public GameObject healthValue;
    private const float maxHealth = 100;
    [SyncVar(hook ="OnHealthChange")]
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnHealthChange(float oldHealth, float newHealth)
    {
        healthValue.transform.localScale = new Vector3((float)(currentHealth / maxHealth),1,1);
       
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            
            if (collision.GetComponent<Bullet>().playerId != netId)
            {
                currentHealth -= collision.GetComponent<Bullet>().damage;
                NetworkServer.Destroy(collision.gameObject);
            }
        }
    }
   
}
