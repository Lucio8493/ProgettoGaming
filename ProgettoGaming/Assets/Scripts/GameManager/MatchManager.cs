using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers {
    public class MatchManager : MonoBehaviour
    {
        // info dallo spawner
        private SpawnManager spnMng;

        //parte del nemico
        protected GameObject[] enemy;
        protected Dictionary<string, string> hunterPrey = new Dictionary<string, string>(); // associazione nome_hunter con nome_hunter
        protected Dictionary<string, string> test = new Dictionary<string, string>();
        private int mexicanStallValue = 3;
        

        // parte del bonus
        protected GameObject[] bonus;
        protected int bonusNumber; // indica il numero di bonus che devono spawnare anche questo deve essere in base alla dimensione del labirinto
        protected Dictionary<GameObject, GameObject> bonusTarget = new Dictionary<GameObject, GameObject>(); // associazione del bonus al target
                                                                                                             // dove il target può essere il cacciatore,
                                                                                                             // la preda o il giocatore

        //parte del player
        protected GameObject player;



        // parte dello score
        // ogni personaggio mantiene il suo punteggio e lo comunica quando muore per la classifica finale.
        // ?invece di fare la classifica si può fare un riepilogo quando il giocatore muore che gli dice il numero di bonus presi
        // e il numero di giocatori catturati?

        // Start is called before the first frame update
        void Start()
        {
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            player = GameObject.FindGameObjectWithTag("Player");

            Debug.Log("Numero di nemici: " + enemy.Length);
            /* per test
            // il primo for mi serve solo per vedere se riuscivo ad accedere al nome dell'oggetto
            for (int i = 0; i < enemy.Length; i++) {
                Debug.Log("MatchManager dice -> Nome nemico in posizione " + i + " è: " + enemy[i].name + "\n");
            }
            
            // per testare se riempie correttamente
            test = AssociatesHunterWithPrey();
            foreach(KeyValuePair<string, string> el in test)
            {
                Debug.Log("" + el.Key + " -> " + el.Value);
            }
            */
            AssociatesHunterWithPrey();
            // per test
            Debug.Log("La preda di Hunter(Clone)2 è: "+GetMyPrey("Hunter(Clone)2"));
            //
        }

        // Update is called once per frame
        void LateUpdate()
        {
            MexicanStall(); // chiamarlo ogni volta che viene notificata una cattura dal nemico o dal giocatore
        }

        // associo l'hunter alla preda
        //private Dictionary<string, string> AssociatesHunterWithPrey() 
        private void AssociatesHunterWithPrey()
        {
            for(int i = 0; i <= enemy.Length; i++)
            {
                // @@ trovare un modo migliore se possibile
                if(i == enemy.Length - 1)
                {
                    hunterPrey.Add(enemy[i].name, player.name);
                    hunterPrey.Add(player.name, enemy[0].name);
                    break;
                }
                hunterPrey.Add(enemy[i].name, enemy[i + 1].name);
            }
            // per testare se associo correttamente.
            //return hunterPrey;
            //
        }

        // se il numero di giocatori è pari a tre cambio le regole della partita
        private void MexicanStall()
        {
            if(hunterPrey.Count == mexicanStallValue)
            {
                Debug.Log("Il primo che cattura il proprio obbiettivo vince");
            }
        }

        // rimuove il valore in base alla chiave hunter passato
        public Dictionary<string, string> TargetCaptured(GameObject hunter)
        {
            hunterPrey.Remove(hunter.name);
            return hunterPrey;
        }

        // prendo la preda che mi è stata associata
        public string GetMyPrey(string nameHunter)
        {
            string namePrey = hunterPrey[nameHunter];
            return namePrey;
        }
    }
}
