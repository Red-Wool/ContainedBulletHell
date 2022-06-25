using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeControlButton : MonoBehaviour
{
    public string keyID;

    public TMP_Text buttonText;

    private void UpdateText(ControlList controls)
    {
        buttonText.text = controls.GetControl(keyID).ToString();
    }

    private void Awake()
    {
        OptionMenu.ChangeControls += UpdateText;
    }
}
