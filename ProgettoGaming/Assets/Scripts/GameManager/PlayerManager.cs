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
        GameObject player; // il giocatore controllato dall'utente


        SwitchColor s; // classe che permette di cambiare il colore della preda e del predatore del giocatore in modo che siano più facilmente riconoscibili

        public void PlayersSet()
        {
            //recupero i vari giocatori
            FindPlayers();
            //nel giocatore principale recupero le arrows
            var children = player.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name == "HunterArrow")
                {
                    HunterArrow = child.GetComponent<Pointing>();
                }
                if (child.name == "PreyArrow")
                {
                    PreyArrow = child.GetComponent<Pointing>();
                }
            }

            //infine setto l'hunter e il prey recuperandoli dallo status del main character
            //changeHunter(player.GetComponent<CharacterStatus>().Hunter);
            //changePrey(player.GetComponent<CharacterStatus>().Prey);
        }

        // imposta i controller che devono utilizzare tutti i personaggi in gioco
        void FindPlayers()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players) {
              
                if ( p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.Player)
                {
                    controllers.Add(p, new KeyboardInputController());
                    player = p;
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
            //changeHunter(player.GetComponent<CharacterStatus>().Hunter);
            //changePrey(player.GetComponent<CharacterStatus>().Prey);


            s = new SwitchColor();

            ChangeColor();

        }


        // cambia il colore della preda e del predatore del giocatore
        void ChangeColor()
        {
            s.HunterColor(player.GetComponent<CharacterStatus>().Hunter);
            s.PreyColor(player.GetComponent<CharacterStatus>().Prey);

        }

        private void LateUpdate()
        {
           // HunterArrow.Point();
            //PreyArrow.Point();
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
    }
}
