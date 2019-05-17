using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManager : MonoBehaviour
    {

        protected FogOfWarScript fow;
        protected SpawnManager spawnManager;

       //Metedo utilizzato per inizializzare l'ambiente
        public void GenerateEnviroment()
        {

            MazeGenerator m = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            m.generate();
            fow = this.GetComponent<FogOfWarScript>();
            fow.Player = GameObject.Find("MainCharacter").GetComponent<Transform>();
            fow.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            fow.UpdateFOW();
        }

    }
}
