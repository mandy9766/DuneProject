using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{

    public AudioClip mainMenuBGM;
    public GameObject stageViewInstance;
    public GameObject stageChoiceViewInstance;
  

  
    void Start()
    {
        SoundManager.Instance.PlayBGM(mainMenuBGM);
        stageViewInstance = UiManager.Instance.ShowView("StageView");
        stageChoiceViewInstance = UiManager.Instance.ShowView("StageChoiceView");
    }
    void Update()
    {
        Debug.Log(DataManager.Instance.nowSlot);
    }
    public void OnClickGotoStartScene()
    {
        ObjectPool.Instance.PoolObject(stageViewInstance);
        ObjectPool.Instance.PoolObject(stageChoiceViewInstance);
        SceneControlManager.Instance.GoStartScene();
    }
    
}
