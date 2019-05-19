using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace InputControllers
{
    public class AiInputController : BaseInputController
    {

        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private NavMeshAgent agent;

        private NavMeshPath path = new NavMeshPath();


        private float ncicliAggiornamento = 10; //@@(time.deltatime) numero di cicli con il quale cambiare direzione (se serve)
        int count = 0;

        public override void CheckInput(GameObject o)
        {
            CharacterStatus status = o.GetComponent<CharacterStatus>();


            // calcolo la strada per arrivare dalla preda
            count++;


            
            // @@ da riscrivere
            if (count > ncicliAggiornamento) { 
             NavMesh.CalculatePath(o.transform.position, status.Prey.transform.position, NavMesh.AllAreas,
              path);
             int cc = 1;
             //Debug.Log(path.corners[cc]);

                Vector3 difference = path.corners[cc] - o.transform.position;

                horz = difference.x;
                 vert = difference.z;
                count = 0;
            }

        }


    }
}

