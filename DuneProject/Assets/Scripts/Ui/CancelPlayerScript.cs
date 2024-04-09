using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelPlayerScript : MonoBehaviour
{
    private int nowSlot;
    public void OnclickCancelPlayerConfirmButton()
    {
        nowSlot = DataManager.Instance.nowSlot;
        DataManager.Instance.DataDelete();
        DataManager.Instance.DataClear();
        EventManager.Instance.PostNotification(EventType.eSlotChanged,this,nowSlot);
        UiManager.Instance.HideAllPanel();
        
    }
}
