using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UI = UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class UnitChoice : MonoBehaviour 
{
    public List<GameObject> prefabsForUnits;
    private List<RectTransform> Units = new List<RectTransform>();
    public ScrollRect scrollRect;
    float space = 50f;
    public UI.Image selectedUnitSlot;

    public void UnitSetting()
    {
        int spicyUnitCount = prefabsForUnits.Count;
        float x = scrollRect.transform.localPosition.x;
        float contentSize = 0f;
        for (int j=0;j<spicyUnitCount;j++)
        {
            var newSpicyUnit = Instantiate(prefabsForUnits[j],scrollRect.content).GetComponent<RectTransform>();
            Units.Add(newSpicyUnit);
            Units[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(x,0);
            x += Units[j].GetComponent<RectTransform>().sizeDelta.x + space;
            contentSize += Units[j].GetComponent<RectTransform>().sizeDelta.x + space;
        }
        scrollRect.content.sizeDelta =new Vector2(contentSize,scrollRect.content.sizeDelta.y);
    }
   
}
