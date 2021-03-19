using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;
public class RegMenu : MonoBehaviour
{
    public GameObject authMenu;
    public GameObject loginInput;
    public GameObject nicknameInput;
    public GameObject emailInput;
    public GameObject password1Input;
    public GameObject password2Input;
    private RegPlayer regPlayer;
    public const string URL = "https://localhost:5001/api/players";
    public void Registration()
    {
        string login = loginInput.GetComponent<InputField>().text;
        string nickname = nicknameInput.GetComponent<InputField>().text;
        string email = emailInput.GetComponent<InputField>().text;
        string password1 = password1Input.GetComponent<InputField>().text;
        string password2 = password2Input.GetComponent<InputField>().text;
        if (password1 != password2)
        {
            Debug.LogError("Password mismatch");
            return;
        }
        regPlayer = new RegPlayer 
        { 
            Login = login,
            GameLogin = nickname,
            Email = email,
            Password = password1 
        };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(regPlayer);
        StartCoroutine(SendRequest(URL, json));
    }
    IEnumerator SendRequest(string url, string json)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success || request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            yield break;
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(request.downloadHandler.text);
            //JSONNode playerDataJSON = JSON.Parse(request.downloadHandler.text);
            gameObject.SetActive(false);
            authMenu.SetActive(true);
        }
    }
}
