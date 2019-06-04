using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class MoveCharacter : MonoBehaviour
    {
       // protected CharacterController _charController;
        protected CharacterStatus status;
        protected CharacterController controller;


        void Start()
        {
            status = GetComponent<CharacterStatus>();
            controller = GetComponent<CharacterController>();           
        }

        private void Update()
        {
            float vertMovement;
            float orizMovement;
            float speed = status.WalkSpeed;

            if (status.IsGrounded)
            {
                if (status.IsMoving )
                {
                    vertMovement = status.VerticalMovement; //asse verticale
                    orizMovement = status.OrizontalMovement;//asse orizzontale

                    //quando premo un pulsante direzionale il protagonista deve direzionarsi in base alla telecamera
                    Vector3 m_move;
                    m_move.z = vertMovement;
                    m_move.x = orizMovement;
                    m_move.y = 0;
                    //gira il personaggio in base a dove deve andare
                    transform.forward = m_move;

                    if (!status.IsFacing)
                    {
                        controller.Move(transform.forward * speed * Time.deltaTime);
                    }
                }
            }
        }
    }
}
