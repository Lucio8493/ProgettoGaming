using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using BonusManager;

namespace GameManagers {


    public class MatchManager : MonoBehaviour
    {
        //parte del nemico
        protected GameObject[] enemy;
        protected List<GameObject> enemyList;
        //nel dictionary entro con la chiave hunter e recupero la prey??
        protected Dictionary<GameObject, GameObject> hunterPrey; // associazione nome_hunter con nome_hunter
        protected Dictionary<string, string> test = new Dictionary<string, string>();
        private int mexicanStallValue = 3;
        private bool InMexicanStall = false;

        List<Bonus> bonuses;
        // tutti i bonus nella partita corrente


        // parte del bonus
        protected GameObject[] bonus;
        // coppia chiave valore, come chiave c'è i personaggi giocanti, come valore il loro bonus
        protected Dictionary<GameObject, Bonus> bonusOfTheCharacter = new Dictionary<GameObject, Bonus>();


        //parte del player
        protected GameObject player;

        // parte dello score
        // ogni personaggio mantiene il suo punteggio e lo comunica quando muore per la classifica finale.
        // ?invece di fare la classifica si può fare un riepilogo quando il giocatore muore che gli dice il numero di bonus presi
        // e il numero di giocatori catturati?


        public void MatchSet()
        {
            hunterPrey = new Dictionary<GameObject, GameObject>();
            //recupero la lista di tutti i giocatori e dallo status recupero l'informazione per capire se sono il protagonista o gli avversari
            enemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject p in enemyList){
                if (p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.Player)
                {
                    player = p;
                    enemyList.Remove(p);
                    break;
                }
            }
            enemy = enemyList.ToArray();

            //Debug.Log("Numero di nemici: " + enemy.Length);
            // per test
            // il primo for mi serve solo per vedere se riuscivo ad accedere al nome dell'oggetto
            for (int i = 0; i < enemy.Length; i++) {
                //Debug.Log("MatchManager dice -> Nome nemico in posizione " + i + " è: " + enemy[i].name + "\n");
            }
            //
            // per testare se riempie correttamente
            AssociatesHunterWithPrey(enemy);
            foreach(KeyValuePair<GameObject, GameObject> el in hunterPrey)
            {
                //Debug.Log("" + el.Key + " -> " + el.Value);
               
               // Debug.Log(Time.realtimeSinceStartup+ " "+ el.Key+ " " + el.Value);
            }

            //
            //AssociatesHunterWithPrey(enemy);
            // per test
           // Debug.Log("La preda di Hunter(Clone)2 è: "+GetMyPrey("Hunter(Clone)2"));
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

        private void OnDestroy()
        {
            Messenger<GameObject, GameObject>.RemoveListener(GameEvent.TARGET_CAPTURED, TargetCaptured);
        }


        void Start()
        {
            
            ReadBonuses rb = new ReadBonuses();
            bonuses = rb.getBonuses();

            Messenger<GameObject, GameObject>.AddListener(GameEvent.TARGET_CAPTURED, TargetCaptured);
        }

        void Update()
        {

            EndCheck();
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
                    hunterPrey.Add(target[i], player);
                    hunterPrey.Add(player, target[0]);
                    break;
                }
                hunterPrey.Add(target[i], target[i + 1]);
            }

            //aggiunta delle varie prede e dei vari hunter negli status dei giocatori
            AssociationsCheck();
            // per testare se associo correttamente.
            //return hunterPrey;
            //


        }

        // se il numero di giocatori è pari a tre cambio le regole della partita
        private void MexicanStall()
        {
            if(hunterPrey.Count == mexicanStallValue)
            {
                //Debug.Log("Il primo che cattura il proprio obbiettivo vince");
                InMexicanStall = true;
            }
        }

        // rimuove il valore in base alla chiave hunter passato
        // @@ vedere se si può migliorare questo metodo
        public void TargetCaptured(GameObject hunter, GameObject prey)
        {
            //verifico l'avvenuta cattura

            //se il maincharacter e' catturato faccio partire la scena di game over
            if (prey == player && hunterPrey[hunter] == player)
            {
                player.GetComponent<CharacterStatus>().IsCaptured = true;
                //Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 3);
            }
            //se il maincharcter cattura faccio partire la scena di vittoria
            else if (hunter == player && hunterPrey[hunter] == prey &&  InMexicanStall)
            {
                player.GetComponent<CharacterStatus>().IsWinning = true;
                //Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 2);
            }
            //altrimenti verifico che la cattura sia lecita e aggiorno gli stati
            else if (hunterPrey[hunter] == prey)
            {
                
                prey.gameObject.GetComponent<CharacterStatus>().IsCaptured = true;
                hunterPrey[hunter] = hunterPrey[prey];
                hunterPrey.Remove(prey);
                enemyList.Remove(prey);
                enemy = enemyList.ToArray();
                
                AssociationsCheck();
            }
        }

        protected void AssociationsCheck()
        {
            foreach (GameObject p in hunterPrey.Keys)
            {
                p.GetComponent<CharacterStatus>().Prey = hunterPrey[p];
                hunterPrey[p].GetComponent<CharacterStatus>().Hunter = p;
                //Debug.Log("hunter: " + p.name + ", prey: "+hunterPrey[p].name);
            }
        }

        // prendo la preda che mi è stata associata
        public GameObject GetMyPrey(GameObject nameHunter)
        {
            GameObject namePrey = hunterPrey[nameHunter];
            return namePrey;
        }

        // restituisce un bonus casuale
        public Bonus getRandomBonus()
        {
            return bonuses[0];
        }

        // assegna il bonus ad un personaggio
        public void assignBonus(GameObject o)
        {
            bonusOfTheCharacter[o] = getRandomBonus();
        }



        public void useBonus(GameObject o)
        {
            o.GetComponent<CharacterStatus>().setBonus(bonusOfTheCharacter[o]);
            StartCoroutine(AnnullaBonus(o));
        }

        // dopo aver aspettato setto i bonus al valore di default
        IEnumerator AnnullaBonus(GameObject o)
        {
            //Debug.Log("inizio conta");

            yield return new WaitForSeconds(bonusOfTheCharacter[o].Seconds);
            //Debug.Log("fine conta");
            o.GetComponent<CharacterStatus>().setBonus(new ReadBonuses().DefaultBonus);

        }

        protected void EndCheck() {

            //se il maincharacter e' morto faccio partire la scena di game over
            if (player.GetComponent<CharacterStatus>().IsDead)
            {
                Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 3);
            }
            //se il maincharcter cattura in mexicanstall faccio partire la scena di vittoria
            else if (player.GetComponent<CharacterStatus>().HasWon)
            {
                Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 2);
            }
        
        }

    }
}
