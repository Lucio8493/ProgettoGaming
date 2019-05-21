using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 1);
    }
}
