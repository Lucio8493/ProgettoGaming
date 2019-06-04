﻿using System;
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
        //protected Dictionary<GameObject, GameObject> hunterPrey; // associazione nome_hunter con nome_hunter
        //private int mexicanStallValue = 3;
        //private bool InMexicanStall = false;
        //private bool OutOfMexicanStall = false;


        // tutti i bonus nella partita corrente
        protected List<Bonus> bonuses;





        // coppia chiave valore, come chiave c'è i personaggi giocanti, come valore il loro bonus
        //protected Dictionary<GameObject, Bonus> bonusOfTheCharacter = new Dictionary<GameObject, Bonus>();

        //parte del player
        //protected GameObject player;


        private MatchStatus MStatus;
        private Rules Rules;

        [SerializeField]
        protected AudioClip mexicanStallSound;

        public void MatchSet()
        {
            GameObject player = null;
            //hunterPrey = new Dictionary<GameObject, GameObject>();
            //recupero la lista di tutti i giocatori e dallo status recupero l'informazione per capire se sono il protagonista o gli avversari
            enemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PLAYER));
            foreach (GameObject p in enemyList){
                if (p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.Player)
                {
                    player = p;
                    enemyList.Remove(p);
                    break;
                }
            }
            enemy = enemyList.ToArray();

            MStatus = new MatchStatus(player);
            Rules = new Rules(MStatus);
            
            Rules.AssociatesHunterWithPrey(enemy);
            MiniMapIconActivation();

            ReadBonuses rb = new ReadBonuses();
            bonuses = rb.getBonuses();

            IstantiateBonuses();

        }

        private void OnDestroy()
        {
            Messenger<GameObject, GameObject>.RemoveListener(GameEventStrings.TARGET_CAPTURED, TargetCaptured);
            Messenger<GameObject, GameObject>.RemoveListener(GameEventStrings.BONUS_PICKED, assignBonusCheck);
        }


        void Start()
        {     
            Messenger<GameObject, GameObject>.AddListener(GameEventStrings.TARGET_CAPTURED, TargetCaptured);
            Messenger<GameObject, GameObject>.AddListener(GameEventStrings.BONUS_PICKED, assignBonusCheck);
        }

        void Update()
        {
            useBonus();
            Rules.EndCheck();
            PlayMexicanStallSound();
        }
        // Update is called once per frame
        void LateUpdate()
        {
            Rules.MexicanStall(); // chiamarlo ogni volta che viene notificata una cattura dal nemico o dal giocatore
        }

        /*
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
        }
        */

        /*
        // se il numero di giocatori è pari a tre cambio le regole della partita
        private void MexicanStall()
        {
            if(hunterPrey.Count < mexicanStallValue)
            {
                OutOfMexicanStall = true;
            }
            if (hunterPrey.Count == mexicanStallValue)
            {
                InMexicanStall = true;
            }
        }
        */

        /*
        // rimuove il valore in base alla chiave hunter passato
        // @@ vedere se si può migliorare questo metodo
        public void TargetCaptured(GameObject hunter, GameObject prey)
        {
            //verifico l'avvenuta cattura

            //se il maincharacter e' catturato faccio partire la scena di game over
            if (prey == player && hunterPrey[hunter] == player)
            {
                player.GetComponent<CharacterStatus>().IsCaptured = true;
            }
            //se il maincharcter cattura faccio partire la scena di vittoria
            else if (hunter == player && hunterPrey[hunter] == prey &&  InMexicanStall)
            {
                player.GetComponent<CharacterStatus>().IsWinning = true;
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
        */

        public void TargetCaptured(GameObject hunter, GameObject prey)
        {
            Rules.TargetCaptured(hunter, prey);
            MiniMapIconActivation();

        }

        /*
            protected void AssociationsCheck()
        {
            foreach (GameObject p in hunterPrey.Keys)
            {
                p.GetComponent<CharacterStatus>().Prey = hunterPrey[p];
                hunterPrey[p].GetComponent<CharacterStatus>().Hunter = p;
            }

            //procedo ad attivare le icone giuste sulla minimappa
            MiniMapIconActivation();

        }
        */

            
        /*
        // prendo la preda che mi è stata associata
        public GameObject GetMyPrey(GameObject nameHunter)
        {
            GameObject namePrey = hunterPrey[nameHunter];
            return namePrey;
        }
        */

        // restituisce un bonus casuale
        public Bonus getRandomBonus()
        {
            return bonuses[UnityEngine.Random.Range(0, bonuses.Count)]; //@@
        }

        
        // assegna il bonus ad un personaggio
        ///nella versione con machStatus e Rules sara' chiamato da li ma il get randomBonus sara' qui
        public void assignBonusCheck(GameObject p, GameObject bonusObject)
        {

            MStatus.ObjectBonusPicked.Add(bonusObject);
            bonusObject.SetActive(false);
            Rules.assignBonus(p, getRandomBonus());
            //bonusOfTheCharacter[o] = getRandomBonus();
        }
        

        public void useBonus()
        {
            foreach (GameObject p in MStatus.GetHunterPreyKeys())
            {
                //Il metodo di verifica e' nelle regole
                if (Rules.UseBonusCheck(p))
                {
                     p.GetComponent<CharacterStatus>().setBonus(MStatus.GetBonusOf(p));
                     StartCoroutine(Rules.ConsumeBonus(p));
                }
            }
        }

        //@@da modificare
        public void IstantiateBonuses()
        {
            foreach (GameObject p in MStatus.GetHunterPreyKeys())
            {
                MStatus.AssignBonus(p, new ReadBonuses().DefaultBonus);
                //bonusOfTheCharacter[p] = new ReadBonuses().DefaultBonus;

            }
        }

        /*
        // dopo aver aspettato setto i bonus al valore di default
        IEnumerator AnnullaBonus(GameObject o)
        {
            yield return new WaitForSeconds(bonusOfTheCharacter[o].Seconds);
            o.GetComponent<CharacterStatus>().setBonus(new ReadBonuses().DefaultBonus);
            bonusOfTheCharacter[o] = new ReadBonuses().DefaultBonus;
            o.GetComponent<CharacterStatus>().UsingBonus = false;
        }
        */

        /*
        protected void EndCheck()
        {
            //se il maincharacter e' morto faccio partire la scena di game over
            if (player.GetComponent<CharacterStatus>().IsDead || OutOfMexicanStall  )
            {
                Messenger<int>.Broadcast(GameEventStrings.CHANGE_SCENE, 4);
            }
            //se il maincharcter cattura in mexicanstall faccio partire la scena di vittoria
            else if (player.GetComponent<CharacterStatus>().HasWon)
            {
                Messenger<int>.Broadcast(GameEventStrings.CHANGE_SCENE, 3);
            }
        }
        */

        //Il metodo serve ad attivare le icone giuste sulla preda e sul cacciatore del giocatore principale
        //@@ il nome dell'oggetto va inserito nel file con le costanti 
        protected void MiniMapIconActivation()
        {
            MStatus.Player.GetComponent<CharacterStatus>().Prey.transform.Find("MiniMapPreyIcon").gameObject.SetActive(true);
            MStatus.Player.GetComponent<CharacterStatus>().Hunter.transform.Find("MiniMapHunterIcon").gameObject.SetActive(true);
        }

        //metodo per l'esecuzione del suono del mexicanStall
        //@@ la clip va tagliata perche' dura 12 secondi ed e' troppo lunga
        protected void PlayMexicanStallSound()
        {
            if (MStatus.InMexicanStall)
            {
                Camera.main.GetComponents<AudioSource>()[1].PlayOneShot(mexicanStallSound);
            }
        }
    }
}
