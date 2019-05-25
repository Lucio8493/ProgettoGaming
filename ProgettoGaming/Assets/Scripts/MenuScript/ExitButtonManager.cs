using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitButtonManager : MonoBehaviour
{

    public void OnClick()
    {
        Messenger.Broadcast(GameEvent.QUIT_MSG);
    }

}
