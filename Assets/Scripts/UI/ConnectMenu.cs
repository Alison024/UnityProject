using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectMenu : MonoBehaviour
{
    public GameObject addressInput;
    public void ConnectedToServer()
    {
        string address = addressInput.GetComponent<InputField>().text;
        GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().networkAddress = address;
        GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().StartClient();
    }
    public void StartHost()
    {
        GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().StartHost();
    }
}
