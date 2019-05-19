using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonManager : MonoBehaviour
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
        Messenger.Broadcast(GameEvent.QUIT_MSG);
    }
}
