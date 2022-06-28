using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using DG.Tweening;

public class StatsManager : MonoBehaviour {
    public static StatsManager instance { get; private set; }

    public RectTransform panel;
    public float bossDefeatTime;
    public float totalTime;
    public int damageTaken;
    public int shotsHit;
    public int totalShotsTaken;

    public TMP_Text bossDefeatTimeText;
    public TMP_Text totalTimeText;
    public TMP_Text damageTakenText;
    public TMP_Text shotHitText;
    public TMP_Text totalShotsText;

    public bool fighting;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    void Start() {
        //DontDestroyOnLoad(gameObject);

        panel.gameObject.SetActive(false);
        fighting = true;
        bossDefeatTime = 0;
        totalTime = 0;
        damageTaken = 0;
        shotsHit = 0;
        totalShotsTaken = 0;
    }

    void Update() {
        if (fighting) bossDefeatTime += Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.I)) StartBattle();
        //if (Input.GetKeyDown(KeyCode.O)) shotsTaken++;
        //if (Input.GetKeyDown(KeyCode.P)) EndBattle();
    }

    public void StartBattle() {
        // hide, start tracking time
        //GetComponent<CanvasGroup>().alpha = 0;
        bossDefeatTime = 0;
        shotsHit = 0;
        fighting = true;
    }

    public void EndBattle() {
        //Stop Music;
        SoundManager.instance.music.loop = false;

        // stop tracking, add to totals
        panel.gameObject.SetActive(true);
        panel.localScale = Vector3.zero;
        panel.DOScale(Vector3.one, 1f).SetEase(Ease.OutCubic);

        fighting = false;
        totalTime += bossDefeatTime;
        //totalShotsTaken += shotsHit;

        // update text
        bossDefeatTimeText.text = "Boss Defeat Time".PadRight(20) + ("" + ((int)bossDefeatTime / 60)).PadLeft(2, '0') + ":" + (bossDefeatTime % 60).ToString("0.00").PadLeft(2, '0');
        totalTimeText.text = "Total Run Time".PadRight(20) + ("" + ((int)totalTime / 60)).PadLeft(2, '0') + ":" + (bossDefeatTime % 60).ToString("0.00").PadLeft(2, '0');
        damageTakenText.text = "Hits Taken".PadRight(20) + (damageTaken == 0 ? "Flawless!" : damageTaken.ToString());
        shotHitText.text = "Shots Taken".PadRight(20) + shotsHit;
        totalShotsText.text = "Total Shots Taken".PadRight(20) + + totalShotsTaken;

        // show
        //GetComponent<CanvasGroup>().alpha = 1;
    }

    public void NextBossButton() {
        DOTween.KillAll();
        Cursor.visible = true;
        string[] sceneNames = new string[] { "Tutorial", "TreeTank", "LighthouseGolem", "OvenFinale" };
        int sceneIndex = Array.IndexOf(sceneNames, SceneManager.GetActiveScene().name);
        if (sceneIndex == -1) {
            Debug.Log("Scene not found :(");
        } else if (sceneIndex == sceneNames.Length - 1) {
            SceneManager.LoadScene("WinScene");
        } else {
            SceneManager.LoadScene(sceneNames[sceneIndex + 1]);
        }
    }
}