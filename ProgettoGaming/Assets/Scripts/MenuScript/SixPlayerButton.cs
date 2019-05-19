using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixPlayerButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Messenger<int>.Broadcast(GameEvent.SETTING_PLAYERS_NUMBER, 6);
    }
}
