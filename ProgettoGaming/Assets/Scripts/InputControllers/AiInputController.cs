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


        private float nFrameAggiornamento = 5;
        int count = 0;

        public override void CheckInput(GameObject o)
        {
            // override with your own code to deal with input
            agent = o.GetComponent<NavMeshAgent>();

            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            agent.autoBraking = false;

            //dico al navmesh che non deve muoversi da solo
            agent.updatePosition = false;
            agent.updateRotation = false;

            // ottengo la posizione della mia preda
            // @ inveve di transform. position si potrebbe usare una "getPosition" di CharacterStatus
            agent.destination = o.GetComponent<CharacterStatus>().getPrey.transform.position;

            count++;

            NavMeshPath path = agent.path;

            if (count > nFrameAggiornamento) {

                //     horz = agent.nextPosition.x.CompareTo(o.transform.position.x);
                int pathx = (int)agent.nextPosition.x;
                int pathz = (int)agent.nextPosition.z;

                if  (pathx.CompareTo((int)o.transform.position.x)  != 0)
                horz = pathx.CompareTo((int) o.transform.position.x);

                if (pathz.CompareTo((int)o.transform.position.z) != 0 )
                vert = pathz.CompareTo((int) o.transform.position.z);
                count = 0;
            }


    }


}
}

