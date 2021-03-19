using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProfileMenu : MonoBehaviour
{
    public void OnPlayerDataChange(PlayerData playerData)
    {
        GameObject.Find("PassedGameValue").GetComponent<Text>().text = playerData.PlayerInfo.PassedGames.ToString();
        GameObject.Find("MaxKillsValue").GetComponent<Text>().text = playerData.PlayerInfo.MaxDamage.ToString();
        GameObject.Find("MaxDamageValue").GetComponent<Text>().text = playerData.PlayerInfo.MaxKills.ToString();
        GameObject.Find("NameText").GetComponent<Text>().text = "Welcome " + playerData.PlayerNickName;
    }
}
