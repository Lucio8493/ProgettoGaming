using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;

namespace Character
{
    public class CharacterStatus : MonoBehaviour
    {
    
        protected PlayerManager playerManagerRef;

        protected bool isRunning;
        protected bool isMoving;
        protected bool isRotating;
        protected bool isGrounded;

        protected float verticalMovement;
        protected float orizontalMovement;

        protected  Vector3 headMovement;

      
        public bool IsRunning{
              get { return isRunning;}

            }
            


        public bool IsMoving
        {
            get { return isMoving; }

        }

     

        //forse isRotating non lo useremo più perchè il personaggio non ruota su se stesso con gli assi orizzontali, ma cammina
        public bool IsRotating
        {
            get { return isRotating; }

        }

        public bool IsGrounded
        {
            get { return isGrounded; }

        }

        public float VerticalMovement
        {
            get { return verticalMovement; }
        }

        public float OrizontalMovement
        {
            get { return orizontalMovement; }
        }

   

        void Start()
        {
            playerManagerRef = GameObject.Find("PlayerManagerObject").GetComponent<PlayerManager>();
 
            isGrounded = true;
        }

        // Update is called once per frame

        void Update()
        {
            checkGrounded();
            CollectInputs();
        }

        protected void CollectInputs()
        {



            /*
            Vector3 ctrlMoves = gameManagerRef.PrimaryInputController.GetMovementDirectionVector();
            verticalMovement = ctrlMoves.z;
            orizontalMovement = ctrlMoves.x;*/

            verticalMovement = playerManagerRef.PrimaryInputController.GetVertical();
            orizontalMovement = playerManagerRef.PrimaryInputController.GetHorizontal();


            isMoving = isGrounded && (playerManagerRef.PrimaryInputController.Down || playerManagerRef.PrimaryInputController.Up
                || playerManagerRef.PrimaryInputController.Left || playerManagerRef.PrimaryInputController.Right);
           // isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);

            isRunning = isMoving && playerManagerRef.PrimaryInputController.useBonus;


        }

        protected void checkGrounded()
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f)
                && hit.collider.CompareTag("Floor"))
                {
                    isGrounded = true;
//                Debug.Log("grounded");
                }
                else
                {
                    isGrounded = false;
            //    Debug.Log("not grounded");
                }

        }

    }
}