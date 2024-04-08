using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControlManager : Singleton<SceneControlManager>
{
    public void GoMainScene()
    {
        HideAllPanel();
        LodingSceneControler.LoadScene("MainScene");
    }
    public void GoStartScene()
    {
        HideAllPanel();
        DataManager.Instance.DataClear();
        LodingSceneControler.LoadScene("StartScene");
    }
    public void HideAllPanel()
    {
        UiManager.Instance.HideAllPanel();
    }
}
