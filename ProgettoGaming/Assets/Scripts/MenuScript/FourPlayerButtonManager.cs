﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourPlayerButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.SETTING_PLAYERS_NUMBER, 4);
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 2);
        Messenger<int>.Broadcast(GameEvent.MAZE_ROWS_DIMENSION, 50);
        Messenger<int>.Broadcast(GameEvent.MAZE_COLUMNS_DIMENSION, 50);
    }
}
