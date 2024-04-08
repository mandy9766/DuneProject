using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
 
public class StageViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    public float space = 50f;
    public GameObject uiPrefab;
    public List<RectTransform> uiObjects = new List<RectTransform>();
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    void Update()
    {
        
    }
    public void AddNewUiObject()
    {
        var newUi = Instantiate(uiPrefab,scrollRect.content).GetComponent<RectTransform>();
        uiObjects.Add(newUi);
        
        float x = scrollRect.transform.localPosition.x + space;
        for (int i = 0; i<uiObjects.Count; i++)
        {
            uiObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();

            if (i%2 != 0)
                uiObjects[i].anchoredPosition = new Vector2(x,-150f);
            else
                uiObjects[i].anchoredPosition = new Vector2(x,150f);

            x += uiObjects[i].sizeDelta.x + space;
        }
        scrollRect.content.sizeDelta = new Vector2(x,scrollRect.content.sizeDelta.y);
    }
}
