﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonManager : MonoBehaviour
{
    public void OnClick()
    {
        Messenger.Broadcast(GameEvent.QUIT_MSG);
    }
}
