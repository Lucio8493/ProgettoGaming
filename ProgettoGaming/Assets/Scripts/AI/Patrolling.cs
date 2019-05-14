using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Patrolling : MonoBehaviour
{
    // togliere i public e mettere i protected
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        //dico al navmesh che non deve muoversi da solo
        agent.updatePosition = false;
        agent.updateRotation = false;

        GotoNextPoint();
    }


    public void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        //nextPosition mi restituisce la prossima posizione da raggiungere

        transform.forward = new Vector3(agent.nextPosition.x.CompareTo(transform.position.x), 0, agent.nextPosition.z.CompareTo(transform.position.z));

        //@@ è un inputcontroller
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.deltaTime * 6); // muovi davanti

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    public void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
        GotoNextPoint();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bonus"))
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Nemico complimenti, hai preso il bonus");
        }
    }
}
