using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageButton : MonoBehaviour
{
    public  TextMeshProUGUI stageName;// Start is called before the first frame update
    public void OnClickStageButton()
    {
        Debug.Log(stageName.text);
        EventManager.Instance.PostNotification(EventType.eOnClickStageButton,this,stageName.text);
    }
}
