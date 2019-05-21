using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BonusManager //restituisce una lista di bonus
{
    public class ReadBonuses
    {
        List<Bonus> bonuses;
        Bonus defaultBonus; // il "bonus" di default che non varia le statistiche (quelli con tutti 1)

        public ReadBonuses()
        {
            bonuses  = new List<Bonus>();

            Bonus b = new Bonus(1);
            b.SpeedBoost = 3f;
            b.Seconds = 4f;

            bonuses.Add(b);
            defaultBonus = new Bonus(0);
        }


        public List<Bonus> getBonuses()
        {
           return bonuses;
        }

        public Bonus DefaultBonus
        {
            get {return defaultBonus;}
        }
    }
}