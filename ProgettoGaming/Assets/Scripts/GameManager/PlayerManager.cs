using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputControllers;


namespace GameManagers
{

    public class PlayerManager : MonoBehaviour
    {
        protected BaseInputController myPrimaryInputController;

        protected GameObject player;
        protected Pointing HunterArrow;
        protected Pointing PreyArrow;


        // Start is called before the first frame update
        void Awake()
        {
            myPrimaryInputController = new KeyboardInputController();
            player = GameObject.FindGameObjectWithTag("Player");
            HunterArrow = GameObject.Find("HunterArrow").GetComponent<Pointing>();
            PreyArrow = GameObject.Find("PreyArrow").GetComponent<Pointing>();
        }

        private void Start()
        {
            setTarget();
        }

        // Update is called once per frame
        void Update()
        {
            myPrimaryInputController.CheckInput();
            
        }

        private void LateUpdate()
        {
            HunterArrow.Point();
            PreyArrow.Point();
        }


        public BaseInputController PrimaryInputController
        {
            get
            {
                return myPrimaryInputController;
            }
        }

        private void changeHunter(GameObject value)
        {
            HunterArrow.Target = value;
        }

        private void changePrey(GameObject value)
        {
            PreyArrow.Target = value;
        }


        //utilizzato temporaneamente per fare le prove il gestore di partita poi settera' prey e hunter
        private void setTarget()
        {
            changeHunter(GameObject.Find("Hunter"));
            changePrey(GameObject.Find("Prey"));
        }

        public void moveCharacter(Vector3 offset)
        {
            player.transform.position = player.transform.position + offset;
        }

        public Vector3 getPosition()
        {
            return player.transform.position;
        }
    }
}
