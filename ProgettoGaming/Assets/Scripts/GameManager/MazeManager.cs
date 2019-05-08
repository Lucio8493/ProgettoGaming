using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManager : MonoBehaviour
    {

        protected FogOfWarScript fow;
        protected SpawnManager spawnManager;

        // Start is called before the first frame update
        void Awake()
        {

            MazeGenerator m = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            m.generate();
            fow = this.GetComponent<FogOfWarScript>();
            spawnManager = this.GetComponent<SpawnManager>();
            spawnManager.Set();

            fow.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            fow.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            fow.UpdateFOW();
        }

    }
}
