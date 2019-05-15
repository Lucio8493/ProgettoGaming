using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuScript : MonoBehaviour
{
    public void SetFourPlayers()
    {
        SettingsClass.NumOfPlayers = 4;
    }

    public void SetSixPlayers()
    {
        SettingsClass.NumOfPlayers = 6;
    }
}
