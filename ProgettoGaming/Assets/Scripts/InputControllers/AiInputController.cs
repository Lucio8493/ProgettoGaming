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

        private NavMeshPath path = new NavMeshPath();

        private float ncicliAggiornamento = 10; //numero di frame dopo i quali ricalcolare il percorso
        int count;
        int cc; // incrementa quando arrivo in un corner e devo arrivare al prossimo


        public AiInputController()
        {
            count = (int)ncicliAggiornamento;
        }

        public override void CheckInput(GameObject o)
        {
            CharacterStatus status = o.GetComponent<CharacterStatus>();

            // calcolo la strada per arrivare dalla preda
            count++;

            if (count > ncicliAggiornamento)
            {
                NavMesh.CalculatePath(o.transform.position, status.Prey.transform.position, NavMesh.AllAreas, path);
                cc = 1;
                count = 0;

                Vector3 difference = path.corners[cc] - o.transform.position;

  
                horz = difference.x;
                vert = difference.z;


            }

        }
    }
}


