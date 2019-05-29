using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManagerTutorial : MonoBehaviour
    {

        protected FogOfWarScript fowTutorial;
        protected SpawnManager spawnManager;
        
        //@@aggiungere i tag alla classe specifica

        //Metedo utilizzato per inizializzare l'ambiente
        public void GenerateEnviromentTutorial()
        {

            fowTutorial = this.GetComponent<FogOfWarScript>();
            fowTutorial.Player = GameObject.Find("MainCharacter").GetComponent<Transform>();
            fowTutorial.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            fowTutorial.UpdateFOW();
        }
    }
}
