using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System;

public class CollisionHandler : MonoBehaviour
{
    protected CharacterController charController;
    protected CharacterStatus status;
    private const string PlayerTag = "Player";
    private const string BonusTag = "Bonus";

    // Start is called before the first frame update
    void Start()
    {
        //charController = this.gameObject.GetComponent<CharacterController>();
        status = this.gameObject.GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string tag =hit.gameObject.tag;
        switch (tag)
        {
            case (PlayerTag):
               
                if (status.Prey == hit.gameObject)
                {
                    Messenger<GameObject,GameObject>.Broadcast(GameEvent.TARGET_CAPTURED, this.gameObject,hit.gameObject);
                }
                break;

            case (BonusTag):
                //Messenger<GameObject>.Broadcast(GameEvent.BUNUS_QUALCOSA, hit.gameObject);
                break;

        }
    }
}
