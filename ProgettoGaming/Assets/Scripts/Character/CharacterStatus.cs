using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;
using BonusManager;

namespace Character
{
    public class CharacterStatus : MonoBehaviour
    {
    
        protected PlayerManager playerManagerRef;



        protected bool isMoving;
        protected bool isRotating;
        protected bool isGrounded;
        protected bool isFacing;
        protected bool isCaptured;
        protected bool isDead;
        protected bool haveBonus;

        protected bool usingBonus;

        protected bool activateBonus;

        private bool isWinning;
        private bool hasWon;

        [SerializeField] protected float walkSpeed = 6.0f;

        protected float speedBoost = 1f;


        
        protected GameObject prey; // la preda che il giocatore deve prendere
        protected GameObject hunter; // la preda che il giocatore deve prendere

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


        public bool UsingBonus
        {
            get { return usingBonus; }
            set { usingBonus = value; }
        }

        public bool ActivateBonus
        {
            get { return activateBonus; }
            set { activateBonus = value; }
        }

        public bool HaveBonus
        {
            get { return haveBonus; }
            set { haveBonus = value; }
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

        public bool IsCaptured
        {
            get { return isCaptured; }
            set { isCaptured = value; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
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
            get { return walkSpeed * speedBoost; }
        }

        public bool IsWinning {
            get => isWinning;
            set => isWinning = value;
        }

        public bool HasWon {
            get => hasWon;
            set => hasWon = value;
        }

        void Start()
        {
            playerManagerRef = GameObject.Find("PlayerManagerObject").GetComponent<PlayerManager>();
 
            isGrounded = true;
            isFacing = false;
            isCaptured = false;
            usingBonus = false;
            activateBonus = false;
        }

        // Update is called once per frame
        void Update()
        {
            checkGrounded();
            checkFacing();
            CollectInputs();
            checkDead();
            checkWin();

            if (playerManagerRef.GetController(this.gameObject).useBonus && usingBonus == false)
            {
                activateBonus = true;
                usingBonus = true;
               // GameObject.Find("MatchManager").GetComponent<MatchManager>().assignBonus(this.gameObject);
               // GameObject.Find("MatchManager").GetComponent<MatchManager>().useBonus(this.gameObject);
            }
        }

        protected void CollectInputs()
        {
            // @posso chiedere  una volta sola getController e salvarla in una variabile
            verticalMovement = playerManagerRef.GetController(this.gameObject).GetVertical();
            orizontalMovement = playerManagerRef.GetController(this.gameObject).GetHorizontal();

            isMoving = isGrounded && (verticalMovement !=0 || orizontalMovement !=0) && !isCaptured;
            // isRotating = isGrounded && (gameManagerRef.PrimaryInputController.Left || gameManagerRef.PrimaryInputController.Right);
        }

        protected void checkGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f)
                && hit.collider.CompareTag("Floor"))
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
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
            }

        }

        protected void checkDead()
        {
            if (isDead == true)
            {
                //Solo quando il personaggio e' morto puo' essere disattivato

                //@@ non credo che la disattivazione vada messa qui
                this.gameObject.SetActive(false);
            }
        }

        // imposta i valori extra secondo il bonus
        public void setBonus(Bonus b)
        {
            speedBoost = b.SpeedBoost;
            activateBonus = false;
        }

        protected void checkWin()
        {
            if (isWinning)
            {
                StartCoroutine(WaitAnimation());
            }
        }

        IEnumerator WaitAnimation()
        {
            //run animation
            yield return new WaitForSeconds(1.1f);
            hasWon = true;
        }
    }

}
