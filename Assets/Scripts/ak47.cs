using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject bulletStartPosition;
    void Start()
    {
        bulletStartPosition = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetBulletStartPosition()
    {
        return bulletStartPosition;
    }
}
