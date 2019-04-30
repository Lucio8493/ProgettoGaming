using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;

namespace Character
{
    public class CharacterStatus : MonoBehaviour
    {
    
        protected GameManager gameManagerRef;

        protected bool isRunning;
        protected bool isMoving;
        protected bool isRotating;
        protected bool isGrounded;
        protected bool isJumping;

        protected float movement;
        protected float rotation;

        protected  Vector3 headMovement;


        public bool IsRunning{
              get { return isRunning;}

            }
            


        public bool IsMoving
        {
            get { return isMoving; }

        }

        public bool IsJumping
        {
            get { return isJumping; }

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

        /*
        public Vector3 HeadMovement
        {
            get { return headMovement; }
        }
        */


        void Start()
        {
            gameManagerRef = GameObject.Find("GameManagerObject").GetComponent<GameManager>().Instance;
 
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


            //headMovement = gameManagerRef.SecondaryInputController.GetMovementDirectionVector();
            //Debug.Log("Head " + headMovement.z);

            Vector3 ctrlMoves = gameManagerRef.PrimaryInputController.GetMovementDirectionVector();
            movement = ctrlMoves.z;
            rotation = ctrlMoves.x;

            isMoving = isGrounded && (gameManagerRef.PrimaryInputController.Down || gameManagerRef.PrimaryInputController.Up);
            isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);

            isRunning = isMoving && gameManagerRef.PrimaryInputController.GetFire(1);

            isJumping = isGrounded && gameManagerRef.PrimaryInputController.GetFire(2);
            //print("is jumping" + isJumping);

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