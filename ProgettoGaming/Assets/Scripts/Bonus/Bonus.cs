using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BonusManager
{

    // la classe bonus è un oggetto che ha solo dei valori, che poi sarebbero i modificatori che il 
    // personaggio ottiene quando lo usa
    public class Bonus
    {
        private int id; // serve per riconoscere i diversi bonus

        private float speedBoost; // il boost che otterrà il giocatore
        private float positionOffset; // l'offset con cui verrà visto dal puntatore del nemico;
        private float seconds; // i secondi nel quale il bonus è attivo

        private bool isMalus; // dice se il bonus è effetivamente un malus, così potrebbe essere usato sul nostro predatore
        
        // costruttore
        public Bonus(int id)
        {
            this.id = id;
            speedBoost = 1;
            positionOffset = 1;
            seconds = 0;
            isMalus = false;
        }


        public int Id
        {
            get { return id;}
        }

        public float SpeedBoost
        {
            get { return speedBoost; }
            set { speedBoost = value; }
        }

        public float PositionOffset
        {
            get { return positionOffset; }
            set { positionOffset = value; }
        }

        public float Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }

        public bool IsMalus
        {
            get { return isMalus; }
        }
    }
}