using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;
using Newtonsoft;
public class AuthMenu : MonoBehaviour
{
    public GameObject profileMenu;
    public GameObject loginInput;
    public GameObject passwordInput;
    public PlayerData playerData;
    public const string URL = "https://localhost:5001/api/authorization/authenticate";
    public void Authenticate()
    {
        string login = loginInput.GetComponent<InputField>().text;
        string password = passwordInput.GetComponent<InputField>().text;
        AuthPlayer authPlayer = new AuthPlayer { Login = login, Password = password };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(authPlayer);
        StartCoroutine(Post(URL, json));
    }
    
    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
        if (request.result != UnityWebRequest.Result.Success || request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error );
            yield break;
        }
        else
        {
            Debug.Log("Form upload complete!");
            JSONNode playerDataJSON = JSON.Parse(request.downloadHandler.text);
            PlayerInfo playerInfo = new PlayerInfo
            {
                Id = playerDataJSON["internalValue"]["playerInfo"]["id"],
                PassedGames = playerDataJSON["internalValue"]["playerInfo"]["passedGames"],
                MaxKills = playerDataJSON["internalValue"]["playerInfo"]["maxKills"],
                MaxDamage = playerDataJSON["internalValue"]["playerInfo"]["maxDamage"]
            };
            Debug.Log(playerInfo);
            playerData = new PlayerData
            {
                PlayerId = playerDataJSON["internalValue"]["id"],
                PlayerLogin = playerDataJSON["internalValue"]["login"],
                PlayerNickName = playerDataJSON["internalValue"]["gameLogin"],
                PlayerInfo = playerInfo
            };
            GameObject.Find("PlayerAuthenticator").GetComponent<PlayerDataStore>().isAuthorize = true;
            this.gameObject.SetActive(false);
            profileMenu.SetActive(true);
            GameObject.Find("PlayerAuthenticator").GetComponent<PlayerDataStore>().PlayerData = playerData;
        }
    }
    
}
