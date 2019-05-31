using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManagerTutorial : MonoBehaviour
    {

        protected FogOfWarScript fow;
        protected SpawnManager spawnManager;
        
        //@@aggiungere i tag alla classe specifica

        //Metedo utilizzato per inizializzare l'ambiente
        public void GenerateEnviromentTutorial()
        {
            fow = this.GetComponent<FogOfWarScript>();
            Debug.Log("fow = this.GetComponent<FogOfWarScript>() -> " + fow);
            fow.Player = GameObject.Find("MainCharacter").GetComponent<Transform>();
            Debug.Log("GameObject.Find(\"MainCharacter\").GetComponent<Transform>() -> " + fow.Player);
            fow.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            fow.UpdateFOW();
        }
    }
}
