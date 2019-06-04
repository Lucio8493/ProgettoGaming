using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class FOWManager : MonoBehaviour
    {

        protected FogOfWarScript fow;
        protected SpawnManager spawnManager;
        
        //Metedo utilizzato per inizializzare l'ambiente
        public void GenerateFOW()
        {
            //MazeGenerator m = GameObject.Find(Names.MAZE).GetComponent<MazeGenerator>();
            //m.generate();
            fow = this.GetComponent<FogOfWarScript>();
            fow.Player = GameObject.Find(Names.MAIN_CHARACTER).GetComponent<Transform>();
            fow.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            fow.UpdateFOW();
        }
    }
}
