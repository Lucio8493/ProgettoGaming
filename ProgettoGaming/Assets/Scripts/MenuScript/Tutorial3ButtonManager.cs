using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3ButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.TUTORIAL_SCENE_3);
    }    
}
