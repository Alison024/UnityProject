using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class WeaponItemSpawner : NetworkBehaviour
{
    private System.Random rnd;
    public GameObject weaponItemSpawnPositions;
    private List<GameObject> weaponItemSpawnPositionsList;
    public GameObject weaponItemPrefab;
    public int weaponItems;

    public override void OnStartServer()
    {
        base.OnStartServer();
        weaponItems = 0;
        rnd = new System.Random();
        weaponItemSpawnPositionsList = new List<GameObject>();
        for (int i = 0; i < weaponItemSpawnPositions.transform.childCount; i++)
        {
            weaponItemSpawnPositionsList.Add(weaponItemSpawnPositions.transform.GetChild(i).gameObject);
        }
        SpawnXItems(5);
    }
    public void SpawnXItems(int x)
    {
        for (int i = 0; i < x; i++)
        {
            int spawnPointIndex = rnd.Next(0, weaponItemSpawnPositionsList.Count - 1);
            Transform transformSpawn = weaponItemSpawnPositionsList[spawnPointIndex].transform;
            GameObject newWeaponItem = Instantiate(weaponItemPrefab, transformSpawn);
            newWeaponItem.transform.parent = null;
            newWeaponItem.GetComponent<WeaponItem>().weaponItemSpawner = this;
            weaponItems++;
            NetworkServer.Spawn(newWeaponItem);
        }
    }
    
}
