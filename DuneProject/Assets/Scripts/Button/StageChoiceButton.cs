using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI = UnityEngine.UI;
using TMPro;

public class StageChoiceButton : MonoBehaviour ,IListener
{
    public  TextMeshProUGUI stageNum;
    public Sprite possibleStageSprite;
    public Sprite impossibleStageSprite;
    public UI.Image image;

    private void Start() 
    {
        SettingStageChoiceButton();
        EventManager.Instance.AddListener(EventType.eGotoMainScene,this);

    }
    public void OnEvent(EventType eventType, Component sender, object Param = null)
    {
        switch(eventType)
        {
            case EventType.eGotoMainScene :
                SettingStageChoiceButton();
                break;
            
        }
    }
   
    public void OnClickStageChoiceButton()
    {
        EventManager.Instance.PostNotification(EventType.eStageClicked,this,int.Parse(stageNum.text));
    }
    public void SettingStageChoiceButton()
    {
        if(DataManager.Instance.VerifyStagePossible(stageNum.text))
            image.sprite = possibleStageSprite;
        else
            image.sprite = impossibleStageSprite;
            
    }
    
}
