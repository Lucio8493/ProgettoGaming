using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsClass
{
  private static int numOfPlayers;
  private static int rowsDimensions;
  private static int columnsDimensions;

    public static int NumOfPlayers
    {
        get => numOfPlayers;
        set => numOfPlayers = value;
    }

    public static int RowsDimensions
    {
        get => rowsDimensions;
        set => rowsDimensions = value;
    }

    public static int ColumnsDimensions
    {
        get => columnsDimensions;
        set => columnsDimensions = value;
    }
}
