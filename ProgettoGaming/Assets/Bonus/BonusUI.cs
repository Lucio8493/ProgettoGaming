using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusUI : MonoBehaviour
{

    Animator anim;
 
    void Start()
    {
        anim = GetComponent<Animator>();
        Messenger<int>.AddListener(GameEventStrings.BONUS_ASSIGNED, ChangeBonusImage);
    }


    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEventStrings.BONUS_ASSIGNED, ChangeBonusImage);

    }

    void ChangeBonusImage(int id)
    {
        anim.SetInteger("BonusId", id);

    }


}
