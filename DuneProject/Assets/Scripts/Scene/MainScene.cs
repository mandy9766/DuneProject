using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{

    public AudioClip mainMenuBGM;

  
    void Start()
    {
        SoundManager.Instance.PlayBGM(mainMenuBGM);
    }
    void Update()
    {
        Debug.Log(DataManager.Instance.nowSlot);
    }
    public void OnClickGotoStartScene()
    {
        SceneControlManager.Instance.GoStartScene();
    }
    
}
