using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixPlayerButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEventStrings.SETTING_PLAYERS_NUMBER, 6);
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.GAME_SCENE);
    }
}
