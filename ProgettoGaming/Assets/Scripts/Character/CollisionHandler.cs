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
    //@@creeremo un solo file di costanti
    private const string PlayerTag = "Player";
    private const string BonusTag = "Bonus";

    // Start is called before the first frame update
    void Start()
    {
        status = this.gameObject.GetComponent<CharacterStatus>();
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
                hit.gameObject.SetActive(false); //@@ spostare nel MatchManager la disattivazione della gemma
                Messenger<GameObject>.Broadcast(GameEvent.BONUS_PICKED, this.gameObject);
                break;
        }
    }
}
