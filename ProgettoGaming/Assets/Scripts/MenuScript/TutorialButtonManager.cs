﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.SETTING_PLAYERS_NUMBER, 3);
        Messenger<int>.Broadcast(GameEvent.MAZE_ROWS_DIMENSION, 50);
        Messenger<int>.Broadcast(GameEvent.MAZE_COLUMNS_DIMENSION, 50);
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 1);
    }    
}
