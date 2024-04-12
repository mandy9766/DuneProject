using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStageChoice : MonoBehaviour
{
   public void OnClickReturnToStageChoice()
    {
        EventManager.Instance.PostNotification(EventType.eReturnToStageChoice,this);
    }
}
