using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsClass
{
  private static int numOfPlayers = 4;

  public static int NumOfPlayers
    {
        get => numOfPlayers;
        set => numOfPlayers = value;
    }
}
