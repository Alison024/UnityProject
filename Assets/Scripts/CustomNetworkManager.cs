using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class CustomNetworkManager : NetworkManager
{
    public GameObject weaponItemSpawnerPrefab;
    private GameObject weaponItemSpawner;
    
    private void Update()
    {
        if(SceneManager.GetActiveScene().name=="Game" && weaponItemSpawner == null && SceneManager.GetActiveScene().isLoaded)
        {
            weaponItemSpawner = Instantiate(weaponItemSpawnerPrefab);
            NetworkServer.Spawn(weaponItemSpawner);
        }
    }

}
