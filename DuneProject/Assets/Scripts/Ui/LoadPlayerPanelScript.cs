using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class LoadPlayerPanelScript : MonoBehaviour
{
    public TextMeshProUGUI[] slotText;

    private void Start() 
    {
        SlotTextSetting();
    }

    public void OnClickLoadPlayerButton(int number)
    {
        DataManager.Instance.SlotSetting(number);
        if(DataManager.Instance.CheckFileExist(number)) // 저장된 데이터가 있을 때 
        {
            DataManager.Instance.LoadData();
            SceneControlManager.Instance.GoMainScene();
        }
        else // 저장된 데이터가 없을 때 
        {
            UiManager.Instance.ShowPanel("NewPlayer",PanelShowBehaviour.HIDE_PREVIOUS);
        }
    }
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
    public void OnClickCancelPlayerButton(int number)
    {
        DataManager.Instance.SlotSetting(number);
        if(DataManager.Instance.CheckFileExist(number)) // 저장된 데이터가 있을 때 
        {
            UiManager.Instance.ShowPanel("CancelPlayer",PanelShowBehaviour.HIDE_PREVIOUS);
        }
        else // 저장된 데이터가 없을 때 
        { 
            Debug.Log("데이터가 없습니다.");
            DataManager.Instance.DataClear();// 저장된 데이터가 없습니다 라는 떴다 사라지는 팝업 출력, 데이터 초기화
        }
    }
    
}
