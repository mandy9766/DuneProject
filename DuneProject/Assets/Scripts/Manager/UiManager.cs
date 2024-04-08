using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
private List<PanelInstanceModel> listInstance = new List<PanelInstanceModel>();
private ObjectPool objectPool;

    void Start()
    {
        objectPool = ObjectPool.Instance;
    }
    public void ShowPanel(string panelId,PanelShowBehaviour behaviour = PanelShowBehaviour.HIDE_PREVIOUS)
    {
        
        GameObject panelInstance = objectPool.GetPanelObjectFromPool(panelId);
        if(panelInstance != null)
        {
            Debug.Log(panelInstance);
            if(behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelIsInQueue()>0)
            {

                var lastPanel = GetLastPanel();
                if(lastPanel != null)
                {
                    lastPanel.panelInstance.SetActive(false);  
                }
            }
            listInstance.Add(new PanelInstanceModel
            {
                panelId = panelId,
                panelInstance = panelInstance
            });
            Debug.Log(listInstance);
        }
        else
        {
            Debug.LogWarning("패널아이디가 없습니다");
        }
    }
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
                if(lastPanel != null && !lastPanel.panelInstance.activeInHierarchy)
                {
                    lastPanel.panelInstance.SetActive(true);
                }
            }
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
