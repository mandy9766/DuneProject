using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;

public class StageButton : MonoBehaviour ,IListener
{
    public  TextMeshProUGUI stageName;// Start is called before the first frame update
    public Sprite possibleStageSprite;
    public Sprite impossibleStageSprite;
    public UI.Image image;

    private void Start() {
        Debug.Log("이거하는거야");
        SettingStageButton();
        EventManager.Instance.AddListener(EventType.eGotoMainScene,this);
    }
    public void OnEvent(EventType eventType, Component sender, object Param = null)
    {
        switch(eventType)
        {
            case EventType.eGotoMainScene :
                SettingStageButton();
                break;
            
        }
    }
    

    public void OnClickStageButton()
    {
        Debug.Log(stageName.text);
        EventManager.Instance.PostNotification(EventType.eOnClickStageButton,this,stageName.text);
    }
    public void SettingStageButton()
    {
        if(DataManager.Instance.GetNowPlayerStage() >= DataManager.Instance.ConvertStageNameToId(stageName.text))
        {
            image.sprite = possibleStageSprite;
        }
        else
        {
            image.sprite = impossibleStageSprite;
        }
            
    }
}
