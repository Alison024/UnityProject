using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerCameraScript : NetworkBehaviour
{
    private GameObject cameraSlot;
    void Start()
    {
        if (isLocalPlayer)
        {
            cameraSlot = transform.Find("CameraSlot").gameObject;
            Transform cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = cameraSlot.transform;
            cameraTransform.position = cameraSlot.transform.position;
            cameraTransform.rotation = cameraSlot.transform.rotation;
            Camera.main.orthographicSize = 6;
        }
    }
    
}
