using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDataStore : MonoBehaviour
{
    public bool isAuthorize = false;
    
    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
        set
        {
            playerData = value;
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                GameObject.Find("PlayerProfile").GetComponent<ProfileMenu>().OnPlayerDataChange(playerData);
            }
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
