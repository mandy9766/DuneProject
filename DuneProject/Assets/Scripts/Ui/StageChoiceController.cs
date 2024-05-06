using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using JetBrains.Annotations;

public class StageChoiceController : MonoBehaviour ,IListener
{
    private ScrollRect scrollRect;
    public GameObject stageChoiceObject;
    public float space = 50f;
    public float verticalSpace = 150f;
    public GameObject stageChoicePrefab;
    public List<RectTransform> stageChoiceObjects = new List<RectTransform>();
    public List<NowStageSetting> nowStageSetting = new List<NowStageSetting>();

    
    

    void Start()
    {
        Debug.Log("이거 실행");
        scrollRect = GetComponent<ScrollRect>();
        StageChoiceSetting();
        EventManager.Instance.AddListener(EventType.eStageClicked,this);
        EventManager.Instance.AddListener(EventType.eReturnToStageChoice,this);
        EventManager.Instance.AddListener(EventType.eGoToStartScene,this);
        EventManager.Instance.AddListener(EventType.eMainSceneStarted,this);


    }
    private void Update() {
        Debug.Log(this.gameObject.name);
        
    }
    
    public void DestroyStage()
    {
        foreach (RectTransform stageChoiceObject in stageChoiceObjects)
        {
            Destroy(stageChoiceObject.gameObject);
        }
        stageChoiceObjects = new List<RectTransform>();
        nowStageSetting = new List<NowStageSetting>();
    }
    public void OnEvent(EventType eventType, Component sender, object Param = null)
    {
        switch(eventType)
        {
            case EventType.eStageClicked :
                HideStageChoice();
                break;
            case EventType.eReturnToStageChoice :
                ShowStageChoice();
                break;
            case EventType.eGoToStartScene :
                DestroyStage();
                break;
            case EventType.eMainSceneStarted :
                StageChoiceSetting();
                break;
        }
    }
    public void ShowStageChoice()
    {
        stageChoiceObject.SetActive(true);
    }
    public void HideStageChoice()
    {
        stageChoiceObject.SetActive(false);
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
}
