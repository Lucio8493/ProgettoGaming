using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagers;

namespace CameraSpace
{

    public class CameraManagerScript : MonoBehaviour
    {

        protected GameManager gameManagerRef;
        [SerializeField] Camera[] cameras;
        protected int currentCamera;




        // Start is called before the first frame update
        void Start()
        {
            gameManagerRef = GameObject.Find("GameManagerObject").GetComponent<GameManager>().Instance;
            currentCamera = 0;
            for(int x=1;x<cameras.Length;x++)
            {
                cameras[x].enabled = false;
            }
        }






        // Update is called once per frame
        void LateUpdate()
        {
            //if(Input.GetKeyDown(KeyCode.C))
                if(gameManagerRef.PrimaryInputController.GetFire(3))
            {
                int oldCamera= currentCamera;
                currentCamera++;
                if(currentCamera>cameras.Length-1)
                {
                    currentCamera = 0;
                }
                cameras[currentCamera].enabled = true;
                cameras[oldCamera].enabled = false;
            }

        }




    }
}
