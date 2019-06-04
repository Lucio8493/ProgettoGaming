using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void FourPlayerButton()
    {
        Messenger<int>.Broadcast(GameEventStrings.SETTING_PLAYERS_NUMBER, 4);
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.GAME_SCENE);
    }

    public void SixPlayerButton()
    {
        Messenger<int>.Broadcast(GameEventStrings.SETTING_PLAYERS_NUMBER, 6);
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.GAME_SCENE);
    }

    public void Tutorial1Button()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.TUTORIAL_SCENE_1);
    }

    public void Tutorial2Button()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.TUTORIAL_SCENE_2);
    }

    public void Tutorial3Button()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.TUTORIAL_SCENE_3);
    }

    public void PlayAgainButton()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneHandler.LastSceneName);
    }

    public void MainMenuButton()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.MENU_SCENE);
    }

    public void ExitButton()
    {
        Messenger.Broadcast(GameEventStrings.QUIT_MSG);
    }
}
