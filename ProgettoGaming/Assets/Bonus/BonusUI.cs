using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusUI : MonoBehaviour
{

    GameObject player = null;
    Animator anim;
    protected CharacterStatus status;



    // Start is called before the first frame update
    void Start()
    {
        var tempList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        foreach (GameObject p in tempList)
        {
            if (p.GetComponent<Character.CharacterStatus>().MyType == Character.CharacterStatus.typeOfPlayer.Player)
            {
                player = p;
                anim = GetComponent<Animator>();
                status = p.GetComponent<CharacterStatus>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (status.UsingBonus)
            anim.SetInteger("BonusId", 1);
        else
            anim.SetInteger("BonusId", 0);


    }
}
