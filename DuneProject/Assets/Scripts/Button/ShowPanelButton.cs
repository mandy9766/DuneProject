using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string panelId;
    public PanelShowBehaviour behaviour;
    private UiManager uiManager;
    void Start()
    {
        uiManager = UiManager.Instance;
    }
    public void DoShowPanel()
    {
        uiManager.ShowPanel(panelId,behaviour);
    }
}
