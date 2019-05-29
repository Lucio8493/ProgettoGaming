using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class SoundHandler : MonoBehaviour
{
    [SerializeField]
    protected AudioClip[] footsteps;
    

    private AudioSource audioSource;
    private CharacterStatus status;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        status = this.GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMoving && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
        }
    }
}
