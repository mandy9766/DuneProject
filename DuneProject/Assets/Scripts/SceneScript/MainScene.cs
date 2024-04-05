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
        // 데이터베이스 volume값 업데이트 및 저장
    }
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(mainMenuBGM);
        volumeSlider.value = SoundManager.Instance.Volume; //나중에 Database로 연동
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickGotoStartScene()
    {
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
