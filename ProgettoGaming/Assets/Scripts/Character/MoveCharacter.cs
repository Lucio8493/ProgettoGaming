using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MoveCharacter : MonoBehaviour
    {
       // protected CharacterController _charController;
        protected CharacterStatus status;
        private Rigidbody rb;


        [SerializeField] protected float walkSpeed = 6.0f;
        [SerializeField] protected float runBoost = 2f;


 

        void Start()
        {
         //   _charController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();

            rb = GetComponent<Rigidbody>();

        }


        private void Update()
        {
            float vertMovement = 0;
            float orizMovement = 0;
            float speed = walkSpeed;

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
                 

                    
                    Vector3 m_move;
                    //   m_move = vertMovement * m_CamForward + orizMovement * m_Cam.right;
                    //gira il personaggio in base a dove deve andare

                    m_move.z = vertMovement;
                    m_move.x = orizMovement;
                    m_move.y = 0;

                    transform.forward = m_move;

                    if (!status.IsFacing)
                    {
                        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed); // muovi davanti
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
    }
}
