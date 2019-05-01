using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class OldMoveCharacter : MonoBehaviour
    {
        protected CharacterController _charController;
        protected CharacterStatus status;

        [SerializeField] protected float walkSpeed = 6.0f;
        [SerializeField] protected float runBoost = 2f;
        [SerializeField] protected float gravity = -9.8f;
        [SerializeField] protected float rotationSensitivity = 9.0f;
        [SerializeField] protected float jumpSpeed = 5.0f;

        protected float ySpeed = 0;
        protected float zSpeed = 0;

        void Start()
        {
            _charController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();
        }


        private void Update()
        {
            Vector3 movement = new Vector3(0, 0, 0);
            float vertMovement = 0;

            if (status.IsGrounded)
            {
                if (status.IsMoving || status.IsJumping)
                {
                    vertMovement = status.Movement * walkSpeed;


                    if (status.IsRunning)
                    {
                        vertMovement *= runBoost;
                    }
                    if (status.IsJumping)
                    {
                        ySpeed += jumpSpeed;
                        zSpeed = transform.InverseTransformDirection(_charController.velocity).z;
                    }
                    else
                    {
                        zSpeed = vertMovement;
                    }
                }
                else
                {
                    ySpeed = 0;
                    zSpeed = 0;
                }

            }
            else
            {
                ySpeed += gravity * Time.deltaTime;
            }

            movement.y += ySpeed * Time.deltaTime;
            movement.z += zSpeed * Time.deltaTime;


            movement = transform.TransformDirection(movement);
            _charController.Move(movement);

            if (status.IsRotating)
            {
                transform.Rotate(0, status.Rotation * rotationSensitivity, 0);
            }

        }
    }
}
