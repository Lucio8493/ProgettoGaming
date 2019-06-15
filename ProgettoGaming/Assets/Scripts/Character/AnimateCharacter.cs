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
        // se il topo è di fronta al muro non fa l'animazione di movimento
        void Update()
        {
            if (status.IsMoving && !status.IsFacing)
            {
                anim.SetInteger("speed", 1);
            } else
            {
                anim.SetInteger("speed", 0);
            }
            checkCapture();
        }

        //@@volendo aggiungere piu' vite si potrebbe utilizzare una variabile in piu' isDying
        void checkCapture()
        {
            if (status.IsCaptured)
            {
                //fai partire l'animazione di morte
                anim.SetBool("IsDead", true);
                //serve ad aspettare la fine dell'animazione
                StartCoroutine(WaitAnimation());
            }
        }

        IEnumerator WaitAnimation()
        {
            //run animation
            yield return new WaitForSeconds(1.1f);
            status.IsDead = true;
        }
    }
}
