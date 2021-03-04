using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerCameraScript : NetworkBehaviour
{
    private GameObject cameraSlot;
    //public Camera camera;
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
    public override void OnStopClient()
    {
        base.OnStopClient();
        Transform cameraTransform = Camera.main.gameObject.transform;
        cameraTransform.parent = null;
        cameraTransform.position = new Vector3(0,0,0);
        cameraTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Camera.main.orthographicSize = 20;
    }
    void Update()
    {
        
    }
    
}
