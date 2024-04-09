using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NewPlayerPanelScript : MonoBehaviour
{
    public  TMP_InputField newPlayerName;
    private int nowSlot;
    
    public void OnClickNewPlayerConfirmButton()
    {
        if(!DataManager.Instance.CheckFileExist(DataManager.Instance.nowSlot))
        {
            nowSlot = DataManager.Instance.nowSlot;
            DataManager.Instance.NewDataSetting(newPlayerName.text);
            DataManager.Instance.SaveData();
            newPlayerName.text="";
            EventManager.Instance.PostNotification(EventType.eSlotChanged,this,nowSlot);
            SceneControlManager.Instance.GoMainScene();//이름 저장하고 나머지 값 초기화해서 게임시작, 클릭한 부분에 데이터 저장

        }
        else
        {
            Debug.Log("이미 파일이 있음 오류");
        }
    }
    
}
