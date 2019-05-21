using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixPlayerButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.SETTING_PLAYERS_NUMBER, 6);
    }
}
