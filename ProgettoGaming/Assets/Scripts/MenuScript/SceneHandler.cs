using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Messenger.AddListener(GameEvent.CHANGE_SCENE, ChangeScene);
        Messenger<int>.AddListener(GameEvent.SETTING_PLAYERS_NUMBER, SetNumberOfPlayers);
        Messenger.AddListener(GameEvent.QUIT_MSG, QuitGame);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
    }

    private void SetNumberOfPlayers(int numOfPlayers)
    {
        SettingsClass.NumOfPlayers = numOfPlayers;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
