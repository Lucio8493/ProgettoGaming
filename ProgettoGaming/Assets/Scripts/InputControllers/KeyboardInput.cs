using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public class KeyboardInput : BaseInputController
    {
        const int NumOfFires = 3;

        bool[] fireInUse;
        string[] axisNames = { "Fire1", "Fire2", "Fire3" };

        public KeyboardInput()
        {
            Fire = new bool[NumOfFires];
            fireInUse = new bool[NumOfFires];
        }

        public override void CheckInput()
        {
            //Debug.Log("checking input");

            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");

            Up = (vert > 0);
            Down = (vert < 0);
            Left = (horz < 0);
            Right = (horz > 0);

            Fire[0] = Input.GetAxis(axisNames[0]) > 0;
            Fire[1] = Input.GetAxis(axisNames[1]) > 0;
            Fire[2] = pressOnce(3);
           
        }

        protected bool pressOnce(int numFire)
        {
            bool temp=false;

            if (Input.GetAxis(axisNames[numFire-1]) != 0)
            {
                if (!fireInUse[numFire-1])
                {
                    temp = true;
                    fireInUse[numFire-1] = true;
                }
                else
                {
                    temp = false;
                }
            }
            else
            {
                fireInUse[numFire-1] = false;
                temp = false;

            }
            return temp;
        }

    }
}
