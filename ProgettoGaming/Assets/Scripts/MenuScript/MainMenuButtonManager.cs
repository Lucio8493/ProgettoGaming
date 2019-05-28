using GameManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.MENU_SCENE);
    }
}
