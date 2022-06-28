using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeControlButton : MonoBehaviour
{
    public string keyID;

    public TMP_Text buttonText;

    private void UpdateText(OptionObject option)
    {
        buttonText.text = option.controls.GetControl(keyID).ToString();
    }

    private void Awake()
    {
        OptionMenu.ChangeControls += UpdateText;
    }
}
