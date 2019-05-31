using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;

namespace GameManagers
{

    public class GameManagerTutorial: MonoBehaviour
    {

        protected static GameManager instance;

        private void Start()
        {
            this.GetComponentInChildren<MazeManagerTutorial>().GenerateEnviromentTutorial();
            this.GetComponentInChildren<MatchManager>().MatchSet();
            this.GetComponentInChildren<PlayerManager>().PlayersSet();
            Camera.main.GetComponent<CameraController>().SetOffset();
        }
    }
}
