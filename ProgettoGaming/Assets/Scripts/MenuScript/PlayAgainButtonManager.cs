using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Play Again dice: nome ultima scena -> " + SceneHandler.LastSceneName);
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneHandler.LastSceneName);
    }
}
