using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListener 
{
    void OnEvent(EventType eventType, Component sender, object Param = null);
}
