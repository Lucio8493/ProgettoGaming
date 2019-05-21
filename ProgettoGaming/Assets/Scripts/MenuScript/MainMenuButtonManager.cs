using GameManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 0);
    }
}
