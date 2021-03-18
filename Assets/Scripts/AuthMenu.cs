using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;
public class AuthMenu : MonoBehaviour
{
    public GameObject loginInput;
    public GameObject passwordInput;
    public PlayerData playerData;
    public const string URL = "https://localhost:5001/api/authorization/authenticate";
    public void Authenticate()
    {
        string login = loginInput.GetComponent<InputField>().text;
        string password = passwordInput.GetComponent<InputField>().text;
        string json = "{\"Login\":" + "\"" + login + "\",\"Password\":" + "\"" + password + "\"}";
        StartCoroutine(Post(URL, json));
    }
    /*public IEnumerator AuthenticatePlayer()
    {
        string login = loginInput.GetComponent<InputField>().text;
        string password = passwordInput.GetComponent<InputField>().text;
        WWWForm form = new WWWForm();

        //form.AddField("Login", login);//Encoding.UTF8
        //form.AddField("Password", password);
        form.AddField("Login", "supper24");//Encoding.UTF8
        form.AddField("Password", "123123");
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success || www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error");
            Debug.Log(login + " " + password);
            Debug.Log(www.error);
            yield break;
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
    }*/
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
            Debug.Log(request.error + " "+ bodyJsonString);
            yield break;
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(request.downloadHandler.text);
        }
    }
    
}
