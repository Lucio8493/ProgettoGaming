﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public class KeyboardInputController : BaseInputController
    {



        public override void CheckInput()
        {
            // override with your own code to deal with input
            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");

            Up = (vert > 0);
            Down = (vert < 0);
            Left = (horz < 0);
            Right = (horz > 0);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                useBonus = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                useBonus = false;
            }



            Debug.Log(useBonus);

        }


    }
}
