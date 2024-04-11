using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using System;
using Unity.VisualScripting;

public class StageViewController : MonoBehaviour ,IListener
{
    private ScrollRect scrollRect;
    public float space = 50f;
    public float verticalSpace = 150f;
    public GameObject uiPrefab;
    public GameObject stageChoicePrefab;
    public GameObject stagePrefab;
    public List<RectTransform> uiObjects = new List<RectTransform>();
    public List<RectTransform> stageChoiceObjects = new List<RectTransform>();
    public List<RectTransform> stageObjects = new List<RectTransform>();
    public List<NowStageSetting> nowStageSetting = new List<NowStageSetting>();
    
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        EventManager.Instance.AddListener(EventType.eStageClicked,this);
        StageChoiceSetting();
    }
    public void OnEvent(EventType eventType, Component sender, object Param = null)
    {
        switch(eventType)
        {
            case EventType.eStageClicked :
                StageSetting((int)Param);
                HideStageChoice();
                break;
        }
    }
    public void ShowStageChoice()
    {
        foreach(RectTransform stageChoice in stageChoiceObjects)
        {
            stageChoice.gameObject.SetActive(true);
        }
        DestroyStage();
    }
    public void HideStageChoice()
    {
        foreach(RectTransform stageChoice in stageChoiceObjects)
        {
            stageChoice.gameObject.SetActive(false);
        }
    }
    public void DestroyStage()
    {
        foreach (RectTransform stageObject in stageObjects)
        {
            Destroy(stageObject.gameObject);
        }
        stageObjects = new List<RectTransform>();
        nowStageSetting = new List<NowStageSetting>();
    }
    public void StageSetting(int num) //nowPlayer의 값과 비교해서 아직 시작할 수 없는 스테이지는 클릭 안되도록 만들기(ui도 잠금한것처럼)
    {
        nowStageSetting = new List<NowStageSetting>();
        int stageCount = DataManager.Instance.GetLastStageId()+1;
        float x = scrollRect.transform.localPosition.x + space;
        for (int j=0;j<stageCount; j++)
        {
            Debug.Log(j);
            if(DataManager.Instance.IsThisStage(j,num))
            {
                var newStage = Instantiate(stagePrefab,scrollRect.content).GetComponent<RectTransform>();
                stageObjects.Add(newStage);
                NowStageSetting newNowStageSetting = new NowStageSetting(j,DataManager.Instance.GetStageName(j));
                nowStageSetting.Add(newNowStageSetting);
                Debug.Log(newNowStageSetting.stageId);
            }
        }
        for (int i = 0; i<stageObjects.Count; i++)
        {
            stageObjects[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = nowStageSetting[i].stageName;

            if (i%2 != 0)
                stageObjects[i].anchoredPosition = new Vector2(x,-verticalSpace);
            else
                 stageObjects[i].anchoredPosition = new Vector2(x,verticalSpace);

            x += stageObjects[i].sizeDelta.x + space;
        }
        scrollRect.content.sizeDelta = new Vector2(x,scrollRect.content.sizeDelta.y);
    }
    public void StageChoiceSetting() //nowPlayer의 값과 비교해서 아직 시작할 수 없는 스테이지는 클릭 안되도록 만들기(ui도 잠금한것처럼)
    {
        int stageNum = DataManager.Instance.GetLastStageNum();
        for (int j=0;j<stageNum; j++)
        {
            var newStage = Instantiate(stageChoicePrefab,scrollRect.content).GetComponent<RectTransform>();
            stageChoiceObjects.Add(newStage);
        }
        float x = scrollRect.transform.localPosition.x + space;
        for (int i = 0; i<stageChoiceObjects.Count; i++)
        {
            stageChoiceObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();

            if (i%2 != 0)
                stageChoiceObjects[i].anchoredPosition = new Vector2(x,-verticalSpace);
            else
                stageChoiceObjects[i].anchoredPosition = new Vector2(x,verticalSpace);

            x += stageChoiceObjects[i].sizeDelta.x + space;
        }
        scrollRect.content.sizeDelta = new Vector2(x,scrollRect.content.sizeDelta.y);
    }
    public void AddNewUiObject()
    {
        var newUi = Instantiate(uiPrefab,scrollRect.content).GetComponent<RectTransform>();
        uiObjects.Add(newUi);
        
        float x = scrollRect.transform.localPosition.x + space;
        for (int i = 0; i<uiObjects.Count; i++)
        {
            uiObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();

            if (i%2 != 0)
                uiObjects[i].anchoredPosition = new Vector2(x,-verticalSpace);
            else
                uiObjects[i].anchoredPosition = new Vector2(x,verticalSpace);

            x += uiObjects[i].sizeDelta.x + space;
        }
        scrollRect.content.sizeDelta = new Vector2(x,scrollRect.content.sizeDelta.y);
    }
    
}

public struct NowStageSetting
{
    public int stageId;
    public string stageName;
    
    public NowStageSetting(int id, string name)
    {
        stageId =id;
        stageName  =name;
    }
}
