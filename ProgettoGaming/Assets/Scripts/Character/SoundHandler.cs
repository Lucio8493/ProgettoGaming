using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class SoundHandler : MonoBehaviour
{
    [SerializeField]
    protected AudioClip die;
    [SerializeField]
    protected AudioClip getBonus;


    private AudioSource audioSource;
    private CharacterStatus status;
    private bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        status = this.GetComponent<CharacterStatus>();
        Messenger<GameObject, GameObject>.AddListener(GameEventStrings.BONUS_PICKED, PlayGetBonus);
    }

    private void OnDestroy()
    {
        Messenger<GameObject, GameObject>.RemoveListener(GameEventStrings.BONUS_PICKED, PlayGetBonus);
    }

    // Update is called once per frame
    void Update()
    {
        PlayDieSound();
    }

    protected void PlayDieSound()
    {
        if (status.IsCaptured && firstTime)
        {
            firstTime = false;
            audioSource.PlayOneShot(die);
        }
    }

    protected void PlayGetBonus(GameObject player, GameObject bonus)
    {
        if (this.gameObject == player)
        {
            audioSource.PlayOneShot(getBonus);
        }
    }
}
