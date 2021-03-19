using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int Id { get; set; }
    public int PassedGames { get; set; }
    public int MaxKills { get; set; }
    public int MaxDamage { get; set; }
    public override string ToString()
    {
        return "PLayerInfo: Id=" + Id + ", Games=" + PassedGames + ", MaxKills=" + MaxKills + ", MaxDamage=" + MaxDamage;
    }
}
