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

        private MatchStatus MStatus;
        private Rules Rules;
        private bool firstTime = true;

        [SerializeField]
        protected AudioClip mexicanStallSound;

        public void MatchSet()
        {
            GameObject player = null;

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


     /*@@M
     * qui procedo ad associare l'hunter e la prey leggendo la matrice da un file.
     * In questo senso si prevede di chimare il costruttore del Matchstatus fornendo come parametri:
     * La matrice, il gameObject mainCharacter e la lista dei gameobject players
     */
            MStatus = new MatchStatus(player);
            Rules = new Rules(MStatus);
            
            Rules.AssociatesHunterWithPrey(enemy);
            MiniMapIconActivation();

            ReadBonuses rb = new ReadBonuses();
            MStatus.BonusTypes= rb.getBonuses();

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

        public void TargetCaptured(GameObject hunter, GameObject prey)
        {
            Rules.TargetCaptured(hunter, prey);
            MiniMapIconActivation();

        }

        // restituisce un bonus casuale
        public Bonus getRandomBonus()
        {
            return MStatus.BonusTypes[UnityEngine.Random.Range(0, MStatus.BonusTypes.Count)]; //@@
        }

        
        // assegna il bonus ad un personaggio
        public void assignBonusCheck(GameObject p, GameObject bonusObject)
        {

            MStatus.ObjectBonusPicked.Add(bonusObject);
            bonusObject.SetActive(false);
            Rules.assignBonus(p, getRandomBonus());
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

        public void IstantiateBonuses()
        {
            foreach (GameObject p in MStatus.GetHunterPreyKeys())
            {
                MStatus.AssignBonus(p, new ReadBonuses().DefaultBonus);
            }
        }

        /*@@M
         * Il metodo funzionera' a patto che gestisca il prey del characterStatus come una lista di gameobject 
         * e non come singolo
         */

        //Il metodo serve ad attivare le icone giuste sulla preda e sul cacciatore del giocatore principale
        protected void MiniMapIconActivation()
        {
            MStatus.MainCharacter.GetComponent<CharacterStatus>().Prey.transform.Find(Names.MINI_MAP_PREY_ICON).gameObject.SetActive(true);
            MStatus.MainCharacter.GetComponent<CharacterStatus>().Hunter.transform.Find(Names.MINI_MAP_HUNTER_ICON).gameObject.SetActive(true);
        }

        //metodo per l'esecuzione del suono del mexicanStall
        //@@ la clip va tagliata perche' dura 12 secondi ed e' troppo lunga
        protected void PlayMexicanStallSound()
        {
            if (MStatus.InMexicanStall && firstTime)
            {
                firstTime = false;
                Camera.main.GetComponents<AudioSource>()[1].PlayOneShot(mexicanStallSound);
            }
        }
    }
}
