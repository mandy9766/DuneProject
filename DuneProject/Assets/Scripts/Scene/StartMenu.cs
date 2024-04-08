using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public AudioClip startMenuBGM;

    void Awake()
    {
    }
    void Start()
    {
        StartCoroutine(StartSettingCoroutine());
        GameManager.Instance.GameManagerLoad();
    }
    void Update()
    {
        Debug.Log(DataManager.Instance.nowSlot);
    }
/// <summary>
/// 처음 게임 시작시 모든 매니저가 정상적으로 로드된 후 사용하기 위한 코루틴
/// </summary>
/// <returns></returns>
    public IEnumerator StartSettingCoroutine()
    {
        yield return null;
        
        SoundManager.Instance.volumeSetting();
        SoundManager.Instance.PlayBGM(startMenuBGM);
        
    }
    public void OnClickQuit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    
}
