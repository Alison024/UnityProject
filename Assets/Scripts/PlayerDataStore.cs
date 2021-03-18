using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataStore : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
