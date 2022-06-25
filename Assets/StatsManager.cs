using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class StatsManager : MonoBehaviour {
    public static StatsManager instance { get; private set; }

    public float bossDefeatTime;
    public float totalTime;
    public int shotsTaken;
    public int totalShotsTaken;

    public bool fighting;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);

        fighting = false;
        bossDefeatTime = 0;
        totalTime = 0;
        shotsTaken = 0;
        totalShotsTaken = 0;
    }

    void Update() {
        if (fighting) bossDefeatTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.I)) StartBattle();
        if (Input.GetKeyDown(KeyCode.O)) shotsTaken++;
        if (Input.GetKeyDown(KeyCode.P)) EndBattle();
    }

    public void StartBattle() {
        // hide, start tracking time
        GetComponent<CanvasGroup>().alpha = 0;
        bossDefeatTime = 0;
        shotsTaken = 0;
        fighting = true;
    }

    public void EndBattle() {
        // stop tracking, add to totals
        fighting = false;
        totalTime += bossDefeatTime;
        totalShotsTaken += shotsTaken;

        // update text
        transform.Find("BossDefeatTimeText").GetComponent<TMP_Text>().text = "Boss Defeat Time".PadRight(20) + ("" + ((int)bossDefeatTime / 60)).PadLeft(2, '0') + ":" + ("" + ((int)bossDefeatTime % 60)).PadLeft(2, '0');
        transform.Find("TotalTimeText").GetComponent<TMP_Text>().text = "Total Run Time".PadRight(20) + ("" + ((int)totalTime / 60)).PadLeft(2, '0') + ":" + ("" + ((int)totalTime % 60)).PadLeft(2, '0');
        transform.Find("ShotsTakenText").GetComponent<TMP_Text>().text = "Shots Taken".PadRight(20) + + shotsTaken;
        transform.Find("TotalShotsTakenText").GetComponent<TMP_Text>().text = "Total Shots Taken".PadRight(20) + + totalShotsTaken;

        // show
        GetComponent<CanvasGroup>().alpha = 1;
    }

    public void NextBossButton() {
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