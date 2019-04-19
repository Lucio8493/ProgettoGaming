using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public class BaseInputController
    {

        // directional buttons
        public bool Up;
        public bool Down;

        public bool Left;
        public bool Right;


        // fire/action buttons
        public bool[] Fire;

        public float vert;
        public float horz;

        public Vector3 TEMPVec3;


        public virtual void CheckInput()
        {
            // override with your own code to deal with input
            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
        }
        public virtual float GetHorizontal()
        {
            return horz;
        }
        public virtual float GetVertical()
        {
            return vert;
        }


        public virtual bool GetFire(int num)
        {
            if (num <= Fire.Length)
            {
                return Fire[num-1];
            }
            else
            {
                return false;
            }
        }



        public virtual Vector3 GetMovementDirectionVector()
        {

            TEMPVec3.x = horz;

            TEMPVec3.z = vert;

            return TEMPVec3;
        }


    }
}