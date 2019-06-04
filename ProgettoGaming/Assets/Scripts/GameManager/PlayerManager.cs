﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;
using Character;
using UnityEngine.AI;

namespace GameManagers
{

    public class PlayerManager : MonoBehaviour
    {

        

        private Dictionary<GameObject, BaseInputController> controllers = new Dictionary<GameObject, BaseInputController>();
        GameObject player; // il giocatore controllato dall'utente


        SwitchColor s; // classe che permette di cambiare il colore della preda e del predatore del giocatore in modo che siano più facilmente riconoscibili

        public void PlayersSet()
        {
            //recupero i vari giocatori
            FindPlayers();

            s = new SwitchColor();

        }

        // imposta i controller che devono utilizzare tutti i personaggi in gioco
        void FindPlayers()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag(Tags.PLAYER);
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

            s = new SwitchColor();

            
            SetColor();

            ChangeColor();

        }


        //setta il colore neutrale a tutti i gicatori in campo tranne al protagonista 
        void SetColor()
        {

            GameObject[] players = GameObject.FindGameObjectsWithTag(Tags.PLAYER);
            foreach (GameObject p in players)
            {
                if (p.GetComponent<CharacterStatus>().MyType != CharacterStatus.typeOfPlayer.Player)
                    s.NeutralColor(p);
            }
        }

            // cambia il colore della preda e del predatore del giocatore
            void ChangeColor()
        {
            s.HunterColor(player.GetComponent<CharacterStatus>().Hunter);
            s.PreyColor(player.GetComponent<CharacterStatus>().Prey);

        }

        public BaseInputController GetController(GameObject p)
        {          
                return controllers[p];         
        }


    }
}
