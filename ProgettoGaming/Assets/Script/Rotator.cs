using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] protected float speedX, speedY, speedZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVector3 = new Vector3(speedX, speedY, speedZ);
        transform.Rotate(rotationVector3 * Time.deltaTime);
    }
}
