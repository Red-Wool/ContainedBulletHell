﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionMenu : MonoBehaviour
{
    public delegate void ChangeControlHandler(ControlList controls);
    public static event ChangeControlHandler ChangeControls;

    public OptionObject options;

    public RectTransform panel;

    public Slider musicSlider;
    public Slider sfxSlider;

    private bool settingControl;
    private string controlID;
    public GameObject setControlPanel;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = options.musicPercent;
        sfxSlider.value = options.sfxPercent;
        UpdateControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (settingControl && Input.anyKeyDown)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                 if (Input.GetKey(kcode))
                 {
                     SetControl(controlID, kcode);
                     settingControl = false;
                     setControlPanel.SetActive(false);
                     
                 }
            }
        }
    }

    public void UpdateControls()
    {
        ChangeControls.Invoke(options.controls);
    }

    public void MusicSlider()
    {
        options.musicPercent = musicSlider.value;
    }

    public void SFXSlider()
    {
        options.sfxPercent = sfxSlider.value;
    }

    public void DisableMenuQuick()
    {
        panel.position = Vector3.down * 500f;
    }

    public void ActivateMenu(bool flag)
    {
        if (flag)
        {
            panel.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        }
        else
        {
            panel.DOLocalMove(Vector3.down * 500f, 1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        }
    }

    public void StartSetControl(string id)
    {
        controlID = id;
        settingControl = true;
        setControlPanel.SetActive(true);
        //options.controls.SetControl(id);
    }

    public void ResetKey(string id)
    {
        options.controls.ResetControl(id);
        ChangeControls.Invoke(options.controls);
    }

    public void SetControl(string id, KeyCode key)
    {
        options.controls.SetControl(id, key);
        ChangeControls.Invoke(options.controls);
    }
}