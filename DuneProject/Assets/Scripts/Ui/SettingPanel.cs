using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Slider volumeSlider;
    private void UpdateVolume(float value)
    {
        SoundManager.Instance.bgSound.volume = value;
        SoundManager.Instance.volumeScrollSetting(value);
        DataManager.Instance.SetVolume(value);
        DataManager.Instance.SaveSetting();
        // 데이터베이스 volume값 업데이트 및 저장
    }
    void OnEnable()
    {
        volumeSlider.value = SoundManager.Instance.Volume; 
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
