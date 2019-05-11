using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers {
    public class MatchManager : MonoBehaviour
    {
        //parte del nemico
        protected GameObject[] enemy;
        protected List<GameObject> enemyList;
        protected Dictionary<string, string> hunterPrey = new Dictionary<string, string>(); // associazione nome_hunter con nome_hunter
        protected Dictionary<string, string> test = new Dictionary<string, string>();
        private int mexicanStallValue = 3;
        
        // parte del bonus
        protected GameObject[] bonus;
        
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
            enemyList = new List<GameObject>(enemy);
            player = GameObject.FindGameObjectWithTag("Player");

            Debug.Log("Numero di nemici: " + enemy.Length);
            // per test
            // il primo for mi serve solo per vedere se riuscivo ad accedere al nome dell'oggetto
            for (int i = 0; i < enemy.Length; i++) {
                Debug.Log("MatchManager dice -> Nome nemico in posizione " + i + " è: " + enemy[i].name + "\n");
            }
            /*
            // per testare se riempie correttamente
            test = AssociatesHunterWithPrey();
            foreach(KeyValuePair<string, string> el in test)
            {
                Debug.Log("" + el.Key + " -> " + el.Value);
            }
            */
            AssociatesHunterWithPrey(enemy);
            // per test
            Debug.Log("La preda di Hunter(Clone)2 è: "+GetMyPrey("Hunter(Clone)2"));
            //

            /*
            // test
            TargetCaptured(enemy[1]);
            for (int i = 0; i < enemy.Length; i++)
            {
                Debug.Log("Dopo il remove -> Nome nemico in posizione " + i + " è: " + enemy[i].name + "\n");
            }
            foreach (KeyValuePair<string, string> el in hunterPrey)
            {
                Debug.Log("Dopo la rimozione");
                Debug.Log("" + el.Key + " -> " + el.Value);
            }
            */
        }

        // Update is called once per frame
        void LateUpdate()
        {
            MexicanStall(); // chiamarlo ogni volta che viene notificata una cattura dal nemico o dal giocatore
        }

        // associo l'hunter alla preda
        //private Dictionary<string, string> AssociatesHunterWithPrey(GameObject[] target) 
        private void AssociatesHunterWithPrey(GameObject[] target)
        {
            for(int i = 0; i <= target.Length; i++)
            {
                // @@ trovare un modo migliore se possibile
                if(i == target.Length - 1)
                {
                    hunterPrey.Add(target[i].name, player.name);
                    hunterPrey.Add(player.name, target[0].name);
                    break;
                }
                hunterPrey.Add(target[i].name, target[i + 1].name);
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
        // @@ vedere se si può migliorare questo metodo
        public void TargetCaptured(GameObject hunter)
        {
            hunterPrey.Remove(hunter.name);
            enemyList.Remove(hunter);
            enemy = enemyList.ToArray();
            hunterPrey.Clear();
            AssociatesHunterWithPrey(enemy);
        }

        // prendo la preda che mi è stata associata
        public string GetMyPrey(string nameHunter)
        {
            string namePrey = hunterPrey[nameHunter];
            return namePrey;
        }


    }
}
