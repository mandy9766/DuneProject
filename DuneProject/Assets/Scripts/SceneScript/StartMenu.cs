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
    void Awake()
    {
        DataManager.Instance.dataManagerInitialize();
    }
    void Start()
    {
        StartCoroutine(StartSettingCoroutine());
        GameManager.Instance.GameManagerLoad();
    }
    void Update()
    {

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
    public void OnClickCancelPlayerButton(int number)
    {
        DataManager.Instance.SlotSetting(number);
        if(DataManager.Instance.CheckFileExist(number)) // 저장된 데이터가 있을 때 
        {
            SwitchCancelPlayerUi();
        }
        else // 저장된 데이터가 없을 때 
        { 
            Debug.Log("데이터가 없습니다.");
            DataManager.Instance.DataClear();// 저장된 데이터가 없습니다 라는 떴다 사라지는 팝업 출력, 데이터 초기화
        }
    }
    public void OnClickNewPlayerConfirmButton()
    {
        if(!DataManager.Instance.CheckFileExist(DataManager.Instance.nowSlot))
        {
            DataManager.Instance.NewDataSetting(newPlayerName.text);
            DataManager.Instance.SaveData();
        }
        GoMainScene();//이름 저장하고 나머지 값 초기화해서 게임시작, 클릭한 부분에 데이터 저장
    }
    public void OnclickCancelPlayerConfirmButton()
    {
        DataManager.Instance.DataDelete();
        DataManager.Instance.DataClear();
        SwitchCancelPlayerUi();
        SwitchLoadPlayerUi();
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
    /// <summary>
    /// LoadPlayer 패널에 표시할 세이브 파일이 있는지 확인하고, 표시하기 위한 함수
    /// </summary>
    private void SlotTextSetting()
    {
        for (int i=0;i<slotText.Length;i++)
        {
            if(DataManager.Instance.CheckFileExist(i+1))
            {
                DataManager.Instance.SlotSetting(i+1);
                DataManager.Instance.LoadData();
                slotText[i].text = DataManager.Instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataManager.Instance.DataClear();
    }
}
