using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int Runic;
    public int boomerRang;

    public PlayerData(PlayerMovement player)
    {
        Runic = player.Runic;
        boomerRang = player.boomerRang;;
    }
}
