using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] protected GameObject lightPlayer;
    private Vector3 lightOffset;

    // Start is called before the first frame update
    void Start()
    {
        lightOffset = transform.position - lightPlayer.transform.position;
    }

    void LateUpdate()
    {
        transform.position = lightPlayer.transform.position + lightOffset;
    }
}
