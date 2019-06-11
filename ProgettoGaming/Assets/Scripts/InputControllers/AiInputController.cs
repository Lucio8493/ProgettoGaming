using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace InputControllers
{
    public class AiInputController : BaseInputController
    {

        // numero piccolo che serve a capire quando sono vicino ad un corner e passare al successivo
        public double epsilon = 0.15;

        private NavMeshPath path = new NavMeshPath();

        int cc; // incrementa quando arrivo in un corner e devo arrivare al prossimo


        // prendo la posizione della preda, arrivo alla posizione della preda che conoscevo, ricalcolo
        public override void CheckInput(GameObject o)
        {
            CharacterStatus status = o.GetComponent<CharacterStatus>();
       


            
            if ( path.corners.Length == cc)
            {
                NavMesh.CalculatePath(o.transform.position, status.Prey.transform.position, NavMesh.AllAreas, path);
                cc = 0;
            }


            Vector3 difference = path.corners[cc] - o.transform.position;

            // se sono molto vicino al corner e se non ho esplorato l'array in tutta lunghezza, vai al prossimo corner

            if (Vector3.Distance(path.corners[cc] , o.transform.position) < epsilon)
            {
                cc++;

            }
            horz = difference.x;
            vert = difference.z;


        }
    }
}

