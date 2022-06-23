using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionMenu : MonoBehaviour
{
    public delegate void ChangeControlHandler(ControlList controls);
    public static event ChangeControlHandler ChangeControls;

    public OptionObject options;

    public GameObject panel;

    private bool settingControl;
    private string controlID;
    public GameObject setControlPanel;

    // Start is called before the first frame update
    void Start()
    {
        ChangeControls.Invoke(options.controls);
    }

    // Update is called once per frame
    void Update()
    {
        if (settingControl)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        options.controls.SetControl(controlID, kcode);
                        settingControl = false;
                        setControlPanel.SetActive(false);
                        ChangeControls.Invoke(options.controls);
                    }
                }

            }
        }
    }

    public void SetControl(string id)
    {
        settingControl = true;
        setControlPanel.SetActive(true);
        //options.controls.SetControl(id);
    }
}
