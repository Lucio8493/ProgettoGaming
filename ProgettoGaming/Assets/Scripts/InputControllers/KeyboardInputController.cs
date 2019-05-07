﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public class KeyboardInputController : BaseInputController
    {

        private Transform m_Cam;                  // A reference to the main camera in the scenes transform


        public override void CheckInput()
        {
            // override with your own code to deal with input
            horz = Input.GetAxisRaw("Horizontal");
            vert = Input.GetAxisRaw("Vertical");


            /* @@*/
            // @@ ATTENZIONE la gestione della camera forse è meglio metterla nel game manager
            if (Camera.main != null)
            {
            m_Cam = Camera.main.transform;
            Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_move = vert * m_CamForward + horz * m_Cam.right;

            horz = m_move.x;
            vert = m_move.z;

            }


            

            Up = (vert > 0);
            Down = (vert < 0);
            Left = (horz < 0);
            Right = (horz > 0);

            useBonus = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0); //  è il pulsante sinistro del mouse





        }


    }
}
