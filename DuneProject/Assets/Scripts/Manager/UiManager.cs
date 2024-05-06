using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    private List<PanelInstanceModel> listInstance = new List<PanelInstanceModel>();
    private List<GameObject> stageButtons = new List<GameObject>();
    private List<GameObject> stageChoiceButtons = new List<GameObject>();
    private ObjectPool objectPool;

    void Start()
    {
        objectPool = ObjectPool.Instance;
    }
    public GameObject ShowStageButton(string stageId)
    {
        GameObject stageInstance = objectPool.GetObjectFromPool(stageId);
        if(stageInstance != null)
        {
            stageButtons.Add(stageInstance);
        }
        return stageInstance;
    }
    public void HideStageButton()
    {
        foreach(GameObject stageButton in stageButtons)
        {
            objectPool.PoolObject(stageButton);
        }
    }
    public GameObject ShowStageChoiceButton(string stageId)
    {
        GameObject stageChoiceInstance = objectPool.GetObjectFromPool(stageId);
        if(stageChoiceInstance != null)
        {
            stageChoiceButtons.Add(stageChoiceInstance);
        }
        return stageChoiceInstance;
    }
    public GameObject ShowView(string viewId)
    {
        GameObject viewInstance = objectPool.GetObjectFromPool(viewId);
        return viewInstance;
        
    }
    public void ShowPanel(string panelId,PanelShowBehaviour behaviour = PanelShowBehaviour.HIDE_PREVIOUS)
    {
        
        GameObject panelInstance = objectPool.GetPanelObjectFromPool(panelId);
        if(panelInstance != null)
        {
            if(behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelIsInQueue()>0)
            {
                // Hide로 선언하여 함수 실행됐을 때 나머지 패널을 전부 비활성화 하는 코드
                for (int i = 0;i<GetAmountPanelIsInQueue();i++)
                {
                    if(listInstance[i] != null)
                        listInstance[i].panelInstance.SetActive(false);
                }

                // 바로 이전 패널만을 비활성화 하도록 하는 코드
                //var lastPanel = GetLastPanel();
                //if(lastPanel != null)
                //{
                //  lastPanel.panelInstance.SetActive(false);  
                //}
            }
            listInstance.Add(new PanelInstanceModel
            {
                panelId = panelId,
                panelInstance = panelInstance
            });
        }
        else
        {
            Debug.LogWarning("패널아이디가 없습니다");
        }
    }
    
    /// <summary>
    /// Warning 팝업을 추가하려면 이곳에 panelId에 조건 추가할 것 
    /// lastPanel의 값을 조회해서 Waring Panel들의 경우 전부 지우고, Warning이 아닌경우 마지막으로 활성화된 패널을 활성화.
    /// </summary>
    public void HideLastPanel()
    {
        if(AnyPanelShowing())
        {
            var lastPanel = GetLastPanel();
            listInstance.Remove(lastPanel);
            objectPool.PoolObject(lastPanel.panelInstance);
            if(GetAmountPanelIsInQueue()>0)
            {
                lastPanel = GetLastPanel();
                if(ThisIsWarningPanel(lastPanel.panelId))
                {
                    HideLastPanel();
                }
                else if(lastPanel != null && !lastPanel.panelInstance.activeInHierarchy)
                {
                    lastPanel.panelInstance.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// Warning 팝업을 추가하려면 이곳에 panelId에 조건 추가할 것 
    /// </summary>
    /// <param name="panelId"></param>
    /// <returns></returns>
    public bool ThisIsWarningPanel(string panelId)
    {
        if(panelId == "NoPlayerWarningPanel")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public PanelInstanceModel GetLastPanel()
    {
        return listInstance[listInstance.Count -1];
    }
    public bool AnyPanelShowing()
    {
        return GetAmountPanelIsInQueue() > 0;
    }
    public int GetAmountPanelIsInQueue()
    {
        return listInstance.Count;
    }
    public void HideAllPanel()
    {
        while(AnyPanelShowing() == true)
        {
            HideLastPanel();
        }
    }
}
