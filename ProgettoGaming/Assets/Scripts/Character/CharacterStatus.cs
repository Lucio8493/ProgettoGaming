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

        [SerializeField] protected float walkSpeed = 6.0f;


        // @@ per prova è passata staticamente, in realtà devo prenderla dinamicamente
        public GameObject prey; // la preda che il giocatore deve prendere
        public GameObject hunter; // la preda che il giocatore deve prendere

        protected float verticalMovement;
        protected float orizontalMovement;

        protected  Vector3 headMovement;

        //definisce se il giocatore deve essere controllato dall'intelligenza artificiale, dal player o da altro
        public enum typeOfPlayer
        {
            Player,
            AI
        }

        //@@ va cambiata al runtime
        [SerializeField]
        protected typeOfPlayer type;

        // dice se sono un giocatore vero, un IA o altro
        public typeOfPlayer MyType
        {
            get { return type; }
            set { type = value; }
        }

        public GameObject Prey
        {
            get { return prey; }
            set { prey = value; }
        }

        public GameObject Hunter
        {
            get { return hunter; }
            set { hunter = value; }
        }

        public bool IsVisible
        {
            get { return IsVisible;  }
            set { IsVisible = value; }
        }

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


        public float WalkSpeed
        {
            get { return walkSpeed; }
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


            // @posso chiedere  una volta sola getController e salvarla in una variabile
            verticalMovement = playerManagerRef.GetController(this.gameObject).GetVertical();
            orizontalMovement = playerManagerRef.GetController(this.gameObject).GetHorizontal();


            isMoving = isGrounded && (verticalMovement !=0 || orizontalMovement !=0);
           // isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);

            // @@ per ora il pulsante del bonus fa muovere solo il personaggio, poi deve usare il bonus preso
            isRunning = isMoving && playerManagerRef.GetController(this.gameObject).useBonus;


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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f)
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