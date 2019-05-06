using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManager : MonoBehaviour
    {

        protected FogOfWarScript fow;

        // Start is called before the first frame update
        void Awake()
        {

            MazeGenerator m = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            m.generate();
            fow = this.GetComponent<FogOfWarScript>();
        }

        // Update is called once per frame
        void Update()
        {
            fow.UpdateFOW();
        }

    }
}
