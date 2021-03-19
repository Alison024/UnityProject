using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataStore : MonoBehaviour
{
    public bool isAuthorize = false;
    public PlayerData playerData;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
