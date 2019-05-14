using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public  class BaseInputController
    {       


        // fire/action buttons
        public bool useBonus; // usa il bonus ottenuto


        public float vert;
        public float horz;



        public virtual void CheckInput(GameObject o)// GameObject è l'oggetto a cui il controller fa riferimento
        {
            // override con il codice per gestire l'input
        
        }




        // ottengo i due assi
        public virtual float GetHorizontal()
        {
            return horz;
        }
        public virtual float GetVertical()
        {
            return vert;
        }






    }
}