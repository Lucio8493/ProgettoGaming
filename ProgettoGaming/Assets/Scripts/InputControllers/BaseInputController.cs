using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public  class BaseInputController
    {

        // directional buttons
        public bool Up;
        public bool Down;

        public bool Left;
        public bool Right;


        // fire/action buttons
        public bool useBonus; // usa il bonus ottenuto


        public float vert;
        public float horz;



        public virtual void CheckInput()
        {
            // override with your own code to deal with input
            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
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