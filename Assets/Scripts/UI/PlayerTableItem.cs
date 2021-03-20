using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTableItem : MonoBehaviour
{
    public GameObject playerNickName;
    public GameObject playerPoints;
    public string playerNName;
    public int playerKills;
    public int playerDeaths;
    public void SetPlayerNickName(string nickName)
    {
        playerNName = nickName;
        playerNickName.GetComponent<Text>().text = nickName;
    }
    public void SetPlayerPoints(int kills, int deaths)
    {
        playerKills = kills;
        playerDeaths = deaths;
        playerPoints.GetComponent<Text>().text = "Kills-"+kills+";Deaths-"+deaths;
    }
}
