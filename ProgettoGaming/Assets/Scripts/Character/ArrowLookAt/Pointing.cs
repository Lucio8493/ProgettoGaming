using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointing : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            rb.transform.LookAt(target.transform.position);
        }
        else
        {
            Debug.Log("Target not set");
        }
    }
}
