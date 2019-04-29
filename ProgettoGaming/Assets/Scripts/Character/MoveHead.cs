using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;
namespace Character
{


    public class MoveHead : MonoBehaviour
    {

        [SerializeField] protected float headRotationSensitivity=1f;
        [SerializeField] protected float headRotationLimitX = 45.0f;
        [SerializeField] protected float headRotationLimitY = 60.0f;
        protected CharacterStatus status;

        protected GameManager gameManagerRef;

        [SerializeField]  protected GameObject headRef;
        protected float rotationX = 0;
        protected float rotationY = 0;

        // Start is called before the first frame update
        void Start()
        {
            //headRef = GameObject.Find("Head");
            status = GetComponent<CharacterStatus>();
            gameManagerRef = GameObject.Find("GameManagerObject").GetComponent<GameManager>().Instance;
        }

        // Update is called once per frame
        void Update()
        {
            Look();
        }

        public void Look()
        {
            float move = gameManagerRef.SecondaryInputController.GetMovementDirectionVector().z;
            //if (Mathf.Abs(status.HeadMovement.z)>0)
            if(move!=0)
            {
                //Debug.Log("is rotating " + rotationX);
                rotationX -= move * headRotationSensitivity;
                rotationX = Mathf.Clamp(rotationX, -headRotationLimitX, headRotationLimitX);
                headRef.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            }
          
        }
    }
}
