using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;

namespace Character
{
    public class CharacterStatusLucio : MonoBehaviour
    {
    
        protected GameManager gameManagerRef;

        protected bool isRunning;
        protected bool isMoving;
        protected bool isRotating;
        protected bool isGrounded;
        protected bool isCaptured;

        //temporaneamente sono stringhe poi definiamo meglio che cacchio di oggetto utilizzare
        protected string prey;
        protected string hunter;

        protected float movement;
        protected float rotation;



        public bool IsRunning{
              get { return isRunning;}

            }
            


        public bool IsMoving
        {
            get { return isMoving; }

        }


        public bool IsRotating
        {
            get { return isRotating; }

        }

        public bool IsGrounded
        {
            get { return isGrounded; }

        }

        public float Movement
        {
            get { return movement; }
        }

        public float Rotation
        {
            get { return rotation; }
        }

        public bool IsCaptured
        {
            get { return isCaptured; }

        }

        public string Prey
        {
            get { return prey; }
        }

        public string Hunter
        {
            get { return hunter; }
        }

        void Start()
        {
            gameManagerRef = GameObject.Find("GameManagerObject").GetComponent<GameManager>().Instance;
 
            isGrounded = true;
            isCaptured = false;
        }

        // Update is called once per frame

        void Update()
        {
            //checkGrounded();
            CollectInputs();
        }

        protected void CollectInputs()
        {

            Vector3 ctrlMoves = gameManagerRef.PrimaryInputController.GetMovementDirectionVector();
            movement = ctrlMoves.z;
            rotation = ctrlMoves.x;

            isMoving = isGrounded && (gameManagerRef.PrimaryInputController.Down || gameManagerRef.PrimaryInputController.Up);
            isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);

            isRunning = isMoving && gameManagerRef.PrimaryInputController.GetFire(1);

        }


        ///Forse per noi e' inutile non credo il personaggio saltera'
//        protected void checkGrounded()
//        {

//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f)
//                && hit.collider.CompareTag("floor"))
//                {
//                    isGrounded = true;
////                Debug.Log("grounded");
//                }
//                else
//                {
//                    isGrounded = false;
//            //    Debug.Log("not grounded");
//                }

//        }

    }
}