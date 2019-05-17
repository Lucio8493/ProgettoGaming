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


   //     [SerializeField] protected float walkSpeed = 6.0f;
        [SerializeField] protected float runBoost = 2f;

        // @@ i bonus non vanno qui
        int numBonus = 0;


        void Start()
        {
         //   _charController = GetComponent<CharacterController>();
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


                    if (status.IsRunning)
                    {
                        speed *= runBoost;
                    }
                 

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
                else
                {
                    vertMovement = 0;
                    orizMovement = 0;
                    speed = 0;
                }
            }
        }

        // @@
        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Bonus"))
            {
                numBonus++;
                hit.gameObject.SetActive(false);
                Debug.Log("Player complimenti, hai preso il bonus. Hai collezionato " + numBonus + " bonus");
            }
        }

    }
}
