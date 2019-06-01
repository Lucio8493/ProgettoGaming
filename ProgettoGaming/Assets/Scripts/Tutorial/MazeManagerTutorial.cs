using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace GameManagers
{
    public class MazeManagerTutorial : MonoBehaviour
    {

        protected FogOfWarScript fow;
        
        //@@aggiungere i tag alla classe specifica

        //Metedo utilizzato per inizializzare l'ambiente
        public void GenerateEnviromentTutorial()
        {


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
