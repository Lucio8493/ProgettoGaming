﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private static string lastSceneName;
    protected static SceneHandler instance;

    public SceneHandler Instance
    {
        get { return instance; }
    }

    public static string LastSceneName
    {
        get => lastSceneName;
        set => lastSceneName = value;
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject); //la scena deve essere ricreata ogni volta
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Messenger<string>.AddListener(GameEventStrings.CHANGE_SCENE, ChangeScene);
        Messenger<int>.AddListener(GameEventStrings.SETTING_PLAYERS_NUMBER, SetNumberOfPlayers);
        Messenger.AddListener(GameEventStrings.QUIT_MSG, QuitGame);
    }

    private void ChangeScene(string nameScene)
    {
        LastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameScene);
    }

    private void SetNumberOfPlayers(int numOfPlayers)
    {
        SettingsClass.NumOfPlayers = numOfPlayers;
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
    }
}
