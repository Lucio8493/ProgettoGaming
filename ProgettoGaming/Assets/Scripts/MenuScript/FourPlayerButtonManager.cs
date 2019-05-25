using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourPlayerButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.SETTING_PLAYERS_NUMBER, 4);
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 1);
    }
}
