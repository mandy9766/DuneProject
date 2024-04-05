using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

    public AudioClip startMenuBGM;
    // Start is called before the first frame update
    void Awake()
    {
        DataManager.Instance.dataManagerInitialize();

    }
    void Start()
    {
        SoundManager.Instance.PlayBGM(startMenuBGM);
        GameManager.Instance.GameManagerLoad();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartGame()
    {
        LodingSceneControler.LoadScene("MainScene");
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
