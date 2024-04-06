using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public GameObject NewPlayerPanel;
    public GameObject CancelPlayerPanel;
    public GameObject LoadPlayerPanel;
    public AudioClip startMenuBGM;
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;
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
    public void OnClickQuit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void OnClickStartGame()
    {
        SlotTextSetting();
        SwitchLoadPlayerUi();
    }
    public void OnClickLoadPlayerButton(int number)
    {
        DataManager.Instance.SlotSetting(number);
        if(DataManager.Instance.CheckFileExist(number)) // 저장된 데이터가 있을 때 
        {
            DataManager.Instance.LoadData();
            GoMainScene();
        }
        else // 저장된 데이터가 없을 때 
        {
            SwitchNewPlayerUi();
        }
    }
    public void OnClickCancelPlayerButton()
    {
        if(true) // 저장된 데이터가 있을 때 
        {
            SwitchCancelPlayerUi();
        }
        else // 저장된 데이터가 없을 때 
        { 
            // 저장된 데이터가 없습니다 라는 떴다 사라지는 팝업 출력
        }
    }
    public void OnClickNewPlayerConfirmButton()
    {
        if(!DataManager.Instance.CheckFileExist(DataManager.Instance.nowSlot))
        {
            DataManager.Instance.nowPlayer.name = newPlayerName.text;
            DataManager.Instance.SaveData();
        }
        GoMainScene();//이름 저장하고 나머지 값 초기화해서 게임시작, 클릭한 부분에 데이터 저장
    }

    
    
    
    public void OnClickCloseNewPlayerUi()
    {
        DataManager.Instance.DataClear();
        SwitchNewPlayerUi();
    }
    public void OnClickCloseCancelPlayerUi()
    {
        DataManager.Instance.DataClear();
        SwitchCancelPlayerUi();
    }
    public void OnClickCloseLoadPlayerUi()
    {
        DataManager.Instance.DataClear();
        SwitchLoadPlayerUi();
    }
    private void SwitchNewPlayerUi()
    {
        NewPlayerPanel.SetActive(!NewPlayerPanel.activeInHierarchy);
    }
    private void SwitchCancelPlayerUi()
    {
        CancelPlayerPanel.SetActive(!CancelPlayerPanel.activeInHierarchy);
    }
    private void SwitchLoadPlayerUi()
    {
        LoadPlayerPanel.SetActive(!LoadPlayerPanel.activeInHierarchy);
    }
    private void GoMainScene()
    {
        LodingSceneControler.LoadScene("MainScene");
    }
    private void SlotTextSetting()
    {
        for (int i=0;i<slotText.Length;i++)
        {
            if(DataManager.Instance.CheckFileExist(i+1))
            {
                DataManager.Instance.SlotSetting(i+1);
                DataManager.Instance.LoadData();
                slotText[i].text = DataManager.Instance.nowPlayer.name;//파일 존재할 경우
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataManager.Instance.DataClear();

    }
    
}
