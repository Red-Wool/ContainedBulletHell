using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class QuipDisplay : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text bodyText;

    private float alphaValue;
    private bool isOn;

    private void Start()
    {
        alphaValue = 0;
        isOn = false;
        //DisplayQuip("His palms are sweaty", "knees weak hard and heavy");
    }

    private void Update()
    {
        alphaValue = Mathf.Clamp(alphaValue + Time.deltaTime * (isOn ? 1 : -1), 0, 1);
        titleText.alpha = alphaValue;
        bodyText.alpha = alphaValue;
    }

    public void DisplayQuip(string title, string body)
    {
        titleText.text = title;
        bodyText.text = body;

        StartCoroutine(TempToggle(3));
    }

    IEnumerator TempToggle(float time)
    {
        isOn = true;

        yield return new WaitForSeconds(time);

        isOn = false;
    }

}
