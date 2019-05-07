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
        private Rigidbody rb;


        [SerializeField] protected float walkSpeed = 6.0f;
        [SerializeField] protected float runBoost = 2f;


        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera

        private int numBonus = 0;

        [SerializeField] protected Text scoreText;

        void Start()
        {
         //   _charController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();

            rb = GetComponent<Rigidbody>();

            SetScoreText();

            // @@ ATTENZIONE la gestione della camera forse è meglio metterla nel game manager invece che in move character, che dovrebbe solo fare qualcosa del tipo GameManager.getCamera
            // o forse la gestione degli assi rispetto alla telecamera è una questione di input, qui non dovrebbe esserci
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
                 

                    //quando premo un pulsante direzionale il protagonista deve direzionarsi in base alla telecamera
                    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    Vector3 m_move;
                    m_move = vertMovement * m_CamForward + orizMovement * m_Cam.right;
                    //gira il personaggio in base a dove deve andare
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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bonus"))
            {
                numBonus++;
                collision.gameObject.SetActive(false);
                Debug.Log("Player complimenti, hai preso il bonus. Hai collezionato " + numBonus + " bonus");
                SetScoreText();
            }
        }

        // @@ è solo per prova
        void SetScoreText()
        {
            scoreText.text = "Bonus raccolti: " + numBonus.ToString();
        }
    }
}
