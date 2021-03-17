using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class WeaponItemSpawner : NetworkBehaviour
{
    private System.Random rnd;
    private GameObject weaponItemSpawnPositions;
    private List<GameObject> weaponItemSpawnPositionsList;
    public GameObject weaponItemPrefab;
    public GameObject weaponParentItems;

    private void Start()
    {
        if (!isServer)
            return;
        rnd = new System.Random();
        weaponItemSpawnPositionsList = new List<GameObject>();
    }
    private void Update()
    {
        if (!isServer)
            return;

        if (GameObject.Find("WeaponSpawnPositions") != null)
        {
            weaponItemSpawnPositions = GameObject.Find("WeaponSpawnPositions");
            for (int i = 0; i < weaponItemSpawnPositions.transform.childCount; i++)
            {
                weaponItemSpawnPositionsList.Add(weaponItemSpawnPositions.transform.GetChild(i).gameObject);
            }
        }
        if (GameObject.Find("WeaponParentItems") != null)
        {
            weaponParentItems = GameObject.Find("WeaponParentItems");
        }
        if (weaponParentItems!=null && weaponParentItems.transform.childCount < 5)
        {
            CmdSpawnXItems();
        }
    }
    public void CmdSpawnXItems()
    {
        int spawnPointIndex = rnd.Next(0, weaponItemSpawnPositionsList.Count - 1);
        Transform transformSpawn = weaponItemSpawnPositionsList[spawnPointIndex].transform;
        GameObject newWeaponItem = Instantiate(weaponItemPrefab, transformSpawn);
        newWeaponItem.transform.parent = weaponParentItems.transform;
        NetworkServer.Spawn(newWeaponItem);
    }
    
}
