using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.CHANGE_SCENE, 1);
    }
}
