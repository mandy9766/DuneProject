using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageChoiceButton : MonoBehaviour
{
   public  TextMeshProUGUI stageNum;
   public void OnClickStageChoiceButton()
    {
        EventManager.Instance.PostNotification(EventType.eStageClicked,this,int.Parse(stageNum.text));
    }
}
