using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonus //restituisce una lista di bonus
{


    public class ReadBonuses
    {
          List<Bonus> bonuses;


        public ReadBonuses()
        {
             bonuses  = new List<Bonus>();

            Bonus b = new Bonus(1);
            b.SpeedBoost = 1.5f;
            b.Seconds = 4f;

            bonuses.Add(b);

        }


        public List<Bonus> getBonuses()
        {
           return bonuses;
        }

    }

}