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
                    //GameObject.Find("MatchManager").GetComponent<MatchManager>().TargetCaptured(this.gameObject, hit.gameObject);
                }
                break;

            case (BonusTag):

                hit.gameObject.SetActive(false); //@@ spostare nel MatchManager


                Messenger<GameObject>.Broadcast(GameEvent.BONUS_PICKED, this.gameObject);

                //CharacterStatus.hasBonus = true;

                break;

        }
    }
}
