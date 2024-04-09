using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControlManager : Singleton<SceneControlManager>
{
    public void GoMainScene()
    {
        UiManager.Instance.HideAllPanel();
        LodingSceneControler.LoadScene("MainScene");
        EventManager.Instance.RemoveRedundancies();
    }
    public void GoStartScene()
    {
        UiManager.Instance.HideAllPanel();
        EventManager.Instance.PostNotification(EventType.eGoToStartScene,this);
        DataManager.Instance.DataClear();
        LodingSceneControler.LoadScene("StartScene");
        EventManager.Instance.RemoveRedundancies();

    }

}
