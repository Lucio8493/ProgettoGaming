using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;

namespace GameManagers
{

    public class GameManager: MonoBehaviour
    {

        protected static GameManager instance;

        private void Start()
        {
            MazeGenerator m = GameObject.Find(Names.MAZE).GetComponent<MazeGenerator>();
            m.generate();
            this.GetComponentInChildren<FOWManager>().GenerateFOW();
            this.GetComponentInChildren<SpawnManager>().SpawnSet();
            this.GetComponentInChildren<MatchManager>().MatchSet();
            this.GetComponentInChildren<PlayerManager>().PlayersSet();
        }
    }
}
