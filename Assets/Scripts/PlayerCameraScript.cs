using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerCameraScript : NetworkBehaviour
{
    public Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
        UpdateCameraState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (camera.enabled == false)
            {
                UpdateCameraState();
            }
        }
    }
    private void UpdateCameraState()
    {
        Camera.main.enabled = false;
        camera.enabled = true;
    }
}
