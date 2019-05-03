using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MoveCharacter : MonoBehaviour
    {
        protected CharacterController _charController;
        protected CharacterStatus status;

        [SerializeField] protected float walkSpeed = 6.0f;
        [SerializeField] protected float runBoost = 2f;
        [SerializeField] protected float gravity = -9.8f;
        [SerializeField] protected float jumpSpeed = 5.0f;


        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera


        protected float ySpeed = 0;
        protected float zSpeed = 0;
        protected float xSpeed = 0;

        void Start()
        {
            _charController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();



            // ** ATTENZIONE ** la gestione della camera forse è meglio metterla nel game manager invece che in move character, che dovrebbe solo fare qualcosa del tipo GameManager.getCamera
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

       



        }


        private void Update()
        {
            Vector3 movement = new Vector3(0, 0, 0);
            float vertMovement = 0;
            float orizMovement = 0;

            if (status.IsGrounded)
            {
                if (status.IsMoving || status.IsJumping)
                {
                    vertMovement = status.VerticalMovement * walkSpeed; //asse verticale
                    orizMovement = status.OrizontalMovement * walkSpeed; //asse orizzontale


                    if (status.IsRunning)
                    {
                        vertMovement *= runBoost;
                        orizMovement *= runBoost;
                    }
                    if (status.IsJumping)
                    {
                        ySpeed += jumpSpeed;
                        zSpeed = transform.InverseTransformDirection(_charController.velocity).z;
                    }
                    else
                    {
                        zSpeed = vertMovement;
                        xSpeed = orizMovement;
                    }

                    //quando premo un pulsante direzionale devo direzionarmi in base alla telecamera
                


                }
                else
                {
                    ySpeed = 0;
                    zSpeed = 0;
                    xSpeed = 0;
                }


            }
            else
            {
                ySpeed += gravity * Time.deltaTime;
            }



            //quando premo un pulsante direzionale il protagonista deve direzionarsi in base alla telecamera
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_move;
            m_move = zSpeed * m_CamForward + xSpeed * m_Cam.right;
            //gira il personaggio in base a dove deve andare
               transform.forward = m_move;
           // _charController.Move(m_Move * Time.deltaTime);

            /*
            movement.y = ySpeed * Time.deltaTime;
            movement.z += zSpeed * Time.deltaTime;
            movement.x += xSpeed * Time.deltaTime;
            */

            Debug.Log(m_move);
            m_move.y = ySpeed;
            Debug.Log(m_move);
            _charController.Move(m_move*Time.deltaTime);

        }
    }
}
