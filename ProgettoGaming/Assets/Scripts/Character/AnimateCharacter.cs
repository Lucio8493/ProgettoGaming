using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class AnimateCharacter : MonoBehaviour
    {

        protected CharacterController _charController;
        protected CharacterStatus status;
        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            _charController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (status.IsMoving)
            {
                anim.SetInteger("speed", 1);
            } else
            {
                anim.SetInteger("speed", 0);

            }
        }
    }
}
