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
        protected bool isFacing;

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

        public bool IsFacing
        {
            get { return isFacing; }
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
            isFacing = false;
        }

        // Update is called once per frame

        void Update()
        {
            checkGrounded();
            checkFacing();
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


            isMoving = isGrounded && (verticalMovement !=0 || orizontalMovement !=0);
           // isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);

            // @@ per ora il pulsante del bonus fa muovere solo il personaggio, poi deve usare il bonus preso
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

        // @@ i valori raw sono un problema
        //mi dice se il personaggio sta di fronte a qualcosa @@ aggiungere eventuale contrllo per il muro
        protected void checkFacing()
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f)
                )
            {
                isFacing = true;
            }
            else
            {
                isFacing = false;
                    //Debug.Log("not facing");
            }

        }


    }
}