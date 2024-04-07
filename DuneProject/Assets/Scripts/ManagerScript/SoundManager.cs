using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    
    public AudioSource bgSound;
    private float volume; 
    public float Volume
    {
        get { return volume; } 
    }
    void Start()
    {

    }
    public void volumeSetting()
    {
            DataManager.Instance.LoadSetting();
            volume = DataManager.Instance.GetVolume();
    }
    public void volumeScrollSetting(float value)
    {
        volume = value;
    }
    public void PlayBGM(AudioClip clip)
    {
        if(GameObject.Find("Bgm") != null)
        {
            bgSound = GameObject.Find("Bgm").GetComponent<AudioSource>();
            bgSound.clip = clip;
            bgSound.loop = true;
            bgSound.volume = volume;
            bgSound.Play();
            Debug.Log("Bgm시작" +clip);
        }
        else
        {
            Debug.Log("Bgm 컴포넌트 Scene에 없음");
        }
    }
}
