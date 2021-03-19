using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject profileMenu;
    public void QuiteGame()
    {
        Application.Quit();
    }
    public void GoBack()
    {
        if (GameObject.Find("PlayerAuthenticator").GetComponent<PlayerDataStore>().isAuthorize)
        {
            profileMenu.SetActive(true);
            
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
