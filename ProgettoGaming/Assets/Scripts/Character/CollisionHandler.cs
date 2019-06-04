using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using GameManagers;
using System;

public class CollisionHandler : MonoBehaviour
{
    protected CharacterController charController;
    protected CharacterStatus status;
    
    // Start is called before the first frame update
    void Start()
    {
        status = this.gameObject.GetComponent<CharacterStatus>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        string tag = hit.gameObject.tag;
        switch (tag)
        {
            case (Tags.PLAYER):
               
                if (status.Prey == hit.gameObject)
                {
                    Messenger<GameObject,GameObject>.Broadcast(GameEventStrings.TARGET_CAPTURED, this.gameObject,hit.gameObject);
                    
                }
                break;

            case (Tags.BONUS):
                status.HaveBonus = true;
                Messenger<GameObject, GameObject>.Broadcast(GameEventStrings.BONUS_PICKED, this.gameObject, hit.gameObject);
                break;
        }
    }
}
