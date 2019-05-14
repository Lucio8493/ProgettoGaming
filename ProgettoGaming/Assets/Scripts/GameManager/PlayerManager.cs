using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;
using Character;
using UnityEngine.AI;

namespace GameManagers
{

    public class PlayerManager : MonoBehaviour
    {

        
        protected Pointing HunterArrow;
        protected Pointing PreyArrow;
        private Dictionary<GameObject, BaseInputController> controllers = new Dictionary<GameObject, BaseInputController>();
        

        // Start is called before the first frame update
        void Start()
        {
            HunterArrow = GameObject.Find("HunterArrow").GetComponent<Pointing>();
            PreyArrow = GameObject.Find("PreyArrow").GetComponent<Pointing>();
            FindPlayers();
            setTarget();
        }

        // imposta i controller che devono utilizzare tutti i personaggi in gioco
        void FindPlayers()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players) {
              
                if ( p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.Player)
                {
                    controllers.Add(p, new KeyboardInputController());
                    p.GetComponent<NavMeshAgent>().enabled = false; // @@soluzione temporanea
                }
                if (p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.AI)
                {
                    controllers.Add(p, new AiInputController());

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

            // controllo gli input di tutti i giocatori
            foreach (GameObject p in controllers.Keys)
            {
                controllers[p].CheckInput(p);
            }
            
        }

        private void LateUpdate()
        {
            HunterArrow.Point();
            PreyArrow.Point();
        }


        public BaseInputController GetController(GameObject p)
        {
           
                return controllers[p];
         
        }


        private void changeHunter(GameObject value)
        {
            HunterArrow.Target = value;
        }

        private void changePrey(GameObject value)
        {
            PreyArrow.Target = value;
        }


        //utilizzato temporaneamente per fare le prove il gestore di partita poi settera' prey e hunter
        private void setTarget()
        {
            changeHunter(GameObject.Find("Hunter"));
            changePrey(GameObject.Find("Prey"));
        }
    }
}
