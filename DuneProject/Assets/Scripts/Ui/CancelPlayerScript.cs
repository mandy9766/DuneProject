using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelPlayerScript : MonoBehaviour
{
    public void OnclickCancelPlayerConfirmButton()
    {
        DataManager.Instance.DataDelete();
        DataManager.Instance.DataClear();
        UiManager.Instance.HideAllPanel();
    }
}
