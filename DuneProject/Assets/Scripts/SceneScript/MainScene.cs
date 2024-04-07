using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public GameObject gamePanel;
    public AudioClip mainMenuBGM;
    public Slider volumeSlider;
    private void UpdateVolume(float value)
    {
        SoundManager.Instance.bgSound.volume = value;
        SoundManager.Instance.volumeScrollSetting(value);
        DataManager.Instance.SetVolume(value);
        DataManager.Instance.SaveSetting();
        // 데이터베이스 volume값 업데이트 및 저장
    }
    void Start()
    {
        SoundManager.Instance.PlayBGM(mainMenuBGM);
        volumeSlider.value = SoundManager.Instance.Volume; 
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }
    void Update()
    {
        
    }
    public void OnClickGotoStartScene()
    {
        DataManager.Instance.DataClear();
        LodingSceneControler.LoadScene("StartScene");
    }
    public void OnClickGameSetting()
    {
        gamePanel.SetActive(!gamePanel.activeInHierarchy);
    }
    public void OnClickGameSettingClose()
    {
        gamePanel.SetActive(!gamePanel.activeInHierarchy);
    }
}
