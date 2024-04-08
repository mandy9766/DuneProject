using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    private UiManager uiManager;
    void Start()
    {
        // Ui매니저 캐싱
        uiManager = UiManager.Instance;
    }
    public void DoHidePanel()
    {
        uiManager.HideLastPanel();
    } 
}
