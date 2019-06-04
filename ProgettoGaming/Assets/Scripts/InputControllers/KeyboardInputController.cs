using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{
    public class KeyboardInputController : BaseInputController
    {

        private Transform m_Cam;  // A reference to the main camera in the scenes transform


        public override void CheckInput(GameObject o)
        {
            // override with your own code to deal with input
            horz = Input.GetAxisRaw("Horizontal");
            vert = Input.GetAxisRaw("Vertical");

            //codice che serve per far andare il personaggio in una direzione, ma rispetto alla telecamera
            // @@ ATTENZIONE prendere la camera dal game manager
            if (Camera.main != null)
            {
            m_Cam = Camera.main.transform;
            Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_move = vert * m_CamForward + horz * m_Cam.right;

            horz = m_move.x;
            vert = m_move.z;
            }

            useBonus = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0); //  è il pulsante sinistro del mouse
        }
    }
}
