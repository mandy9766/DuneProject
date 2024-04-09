using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningPanel : MonoBehaviour
{
    private float fadeDuration = 0.8f;
    public GameObject warningPanel;
    private Image image;
    private Color color;
    private float startAlpha;
    private float FixedAlpha;

    public TextMeshProUGUI text;
    private Color textColor;

    private void Awake() 
    {
        image = warningPanel.GetComponent<Image>();
        color = warningPanel.GetComponent<Image>().color;
        FixedAlpha = color.a;
        textColor = text.color;
    }
    private void OnEnable() {
        StartCoroutine(FadeOut());
    }
    public IEnumerator FadeOut()
    {
        startAlpha = FixedAlpha;
        color.a = FixedAlpha;
        textColor.a = FixedAlpha;
        image.color = color;
        text.color = textColor;
        for (float t = 0.0f; t<1.0f; t += Time.deltaTime/fadeDuration)
        {
            color.a = Mathf.Lerp(startAlpha,0,t);
            textColor.a = Mathf.Lerp(startAlpha,0,t);
            image.color = color;
            text.color = textColor;
            yield return null;
        }
        UiManager.Instance.HideLastPanel();
    }


}
