using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointing : MonoBehaviour
{
    //[SerializeField]
    private GameObject target;
    private Rigidbody rb;

    public GameObject Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    public void Point()
    {
        if (target)
        {
            rb.transform.LookAt(Target.transform.position);
        }
        else
        {
            Debug.Log("Target not set");
        }
    }
}
