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
        Messenger.AddListener(GameEvent.SETTING_PLAYERS_NUMBER, SetNumberOfPlayers);
    }

    private void SetNumberOfPlayers()
    {
        SettingsClass.NumOfPlayers = 6;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
