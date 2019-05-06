using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;

namespace GameManagers
{

    public class GameManager: MonoBehaviour
    {

        protected static GameManager instance;
        protected BaseInputController myPrimaryInputController;
        protected FogOfWarScript fow;

        public GameManager Instance
        {
            get { return instance; }
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
            DontDestroyOnLoad(gameObject);

            instance.myPrimaryInputController = new KeyboardInputController();

            //Call the InitGame function to initialize the level
           InitGame();
        }

        void InitGame()
        {
            //Call the SetupScene function of the BoardManager script
            MazeGenerator m = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            m.generate();
            fow = GameObject.Find("FOWObject").GetComponent<FogOfWarScript>();
        }

        public BaseInputController PrimaryInputController
        {
            get
            {
                return myPrimaryInputController;
            }
        }

  

        //Update is called every frame.
        void Update()
        {
            myPrimaryInputController.CheckInput();
            fow.UpdateFOW();
        }

    }


}
