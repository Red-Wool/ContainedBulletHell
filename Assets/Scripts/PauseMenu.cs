using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    private KeyCode pause = KeyCode.P;

    public GameObject pausePanel;

    public RectTransform warningPanel;
    public TMP_Text warningText;
    private bool isQuitting;

    public OptionMenu options;

    private bool isPaused = false;
    private int currentTime = 0;

    private void SetControl(OptionObject option)
    {
        pause = option.controls.GetControl("Pause");
    }

    private void Awake()
    {
        OptionMenu.ChangeControls += SetControl;
    }

    private void Start()
    {
        options.UpdateControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            PauseState();   
        }
    }

    public void IssueWarning(bool isQuit)
    {
        isQuitting = isQuit;
        warningText.text = "Are you sure you want to " + (isQuitting ? "quit?\n\nProgress is not saved" : "restart?");
        ActivateWarning(true);
    }

    public void ActivateWarning(bool flag)
    {
        if (flag)
        {
            warningPanel.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic).SetUpdate(true);
            warningPanel.DOLocalRotate(Vector3.forward * 1080f, 1f).SetUpdate(true);
        }
        else
        {
            warningPanel.DOScale(Vector3.zero, 1f).SetEase(Ease.OutCubic).SetUpdate(true);
            warningPanel.DOLocalRotate(Vector3.zero, 1f).SetUpdate(true);
        }
    }

    public void QuitButton()
    {
        isPaused = true;
        PauseState();
        ButtonBlock.ResetButtonPress();
        SceneManager.LoadScene(isQuitting ? "Title" : SceneManager.GetActiveScene().name);
    }

    public void PauseState()
    {
        if (isPaused)
        {
            ScenePause.instance.activeScene = currentTime;
            Time.timeScale = 1f;
            options.DisableMenuQuick();
            warningPanel.localScale = Vector3.zero;
        }
        else
        {
            currentTime = ScenePause.instance.activeScene;
            ScenePause.instance.activeScene = 2;
            Time.timeScale = 0f;
        }

        isPaused = !isPaused;
        SoundManager.instance.PauseMusic(isPaused);
        pausePanel.SetActive(isPaused);
    }
}
