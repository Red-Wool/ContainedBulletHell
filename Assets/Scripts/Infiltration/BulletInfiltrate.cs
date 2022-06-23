using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class BulletInfiltrate : MonoBehaviour {
    public static BulletInfiltrate instance { get; private set; }

    public PlayerMove player;
    public BossShoot boss;
    public QuipDisplay quip;
    public GameObject portal;
    public GameObject container;

    public BossHPBar bossBar;

    private List<BulletWeaken> weakBullets;
    private BulletWeaken bulletSceneName;
    public Vector3 lastPos { get; private set; }

    [HideInInspector] public InfiltrateStart scaler;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

     //instantly kill the boss cheats
     void Update() {
          if (Input.GetKeyDown(KeyCode.U)) StartCoroutine(Explosions());
     }
     

    public void AddWeakenBullet(BulletWeaken[] weak) {
        weakBullets = new List<BulletWeaken>();
        foreach (BulletWeaken i in weak) {
            weakBullets.Add(i);
        }
    }

    public bool CheckGetAll() {
        if (weakBullets.Count == 0 && !boss.Final) {
            boss.Second();
            bossBar.ChangeBar();
            return true;
        }
        return false;
    }

    public void Victory() {
        for (int i = 0; i < weakBullets.Count; i++) {
            if (weakBullets[i].sceneName == bulletSceneName.sceneName) {
                weakBullets[i].display.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutCubic);
                weakBullets.Remove(weakBullets[i]);
            }
        }
    }

    public bool CheckBullet(Transform obj, int id) {
        if (ScenePause.instance.activeScene == 1)
        {
            return false;
        }
        for (int i = 0; i < weakBullets.Count; i++) {
            if (id == weakBullets[i].weakID) {
                ActivateBulletInfiltration(obj, weakBullets[i]);
                return true;
            }
        }
        return false;
    }

    public void BossPortal() {
        ActivateBulletInfiltration(boss.transform, boss.finalScene);
    }

    public void ActivateBulletInfiltration(Transform obj, BulletWeaken sceneName) {
        //Debug.Log(sceneName + " Activated111!!");
        portal.GetComponent<InfiltratePortal>().scene = sceneName;
        SpawnPortal(obj, true);
    }

    public void SpawnPortal(Transform obj, bool flag) {
        if (flag) {
            portal.GetComponent<BoxCollider2D>().enabled = true;
            portal.transform.position = obj.position;
            portal.transform.parent = obj;
            portal.transform.localScale = Vector3.zero;
            portal.transform.DOScale(Vector3.one, 1f);
        } else {
            Transform parent = portal.transform.parent;

            portal.GetComponent<BoxCollider2D>().enabled = false;
            portal.transform.parent = null;
            portal.transform.DOScale(Vector3.zero, 1f);
            lastPos = parent.transform.position;

            if (!boss.Final)
                parent.gameObject.SetActive(false);
        }
    }

    public void EnterBullet(BulletWeaken sceneName) {
        EvalutePattern.instance.StopAllPatterns();
        SpawnPortal(transform, false);
        ScenePause.instance.activeScene = 1;
        SceneManager.LoadSceneAsync(sceneName.sceneName, LoadSceneMode.Additive);

        bulletSceneName = sceneName;

        StartCoroutine(MoveAway());
    }

    public IEnumerator MoveAway() {
        container.transform.DOScale(Vector3.one * 10f, 5f);
        //container.transform.DOMove(Vector3.down * 100, 5f).SetEase(Ease.OutExpo);
        
        yield return new WaitForSeconds(2.5f);

        player.InfiltrateTransition(lastPos, false);
        SoundManager.instance.infiltrateOut.Play();
        container.transform.position = Vector3.down * 100000;

        yield return new WaitForSeconds(3f);

        quip.DisplayQuip(bulletSceneName.enterText, bulletSceneName.subText);
    }

    public IEnumerator MoveIn() {
        //container.transform.position = Vector3.down * 100;
        container.transform.position = Vector3.zero;
        container.transform.DOScale(Vector3.one, 2f);
        yield return new WaitForSeconds(2f);
        SoundManager.instance.infiltrateOut.Play();
        player.InfiltrateTransition(lastPos, false);

        yield return new WaitForSeconds(3f);
        if (boss.Phase2 && !boss.Final) {
            yield return new WaitForSeconds(4f);
        } else if (boss.Final) {
            player.Heal(8);
            
            ParticleManager.instance.Toggle(ParticleManager.instance.stars, false);
            StartCoroutine(Explosions());
        }

        ScenePause.instance.activeScene = 0;
        //SceneManager.UnloadSceneAsync(bulletSceneName);

    }

    public IEnumerator Explosions() {
        DOTween.KillAll();
        boss.transform.DOShakePosition(5, 10, 10, 90);
        for (int i = 0; i < 30; i++) {
            SoundManager.instance.explosion.Play();
            ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, boss.transform.position + UtilFunctions.Vec2ToVec3(UtilFunctions.RandVec2Range(Vector2.one * 5f)));
            yield return new WaitForSeconds(0.15f);
        }
        boss.gameObject.SetActive(false);

        //Double Kill all just in case!
        DOTween.KillAll();
        string[] sceneNames = new string[] {"Tutorial", "TreeTank", "LighthouseGolem", "OvenFinale" };
        int sceneIndex = Array.IndexOf(sceneNames, SceneManager.GetActiveScene().name);
        if (sceneIndex == -1) {
            Debug.Log("Scene not found :(");
        } else if (sceneIndex == sceneNames.Length - 1) {
            SceneManager.LoadScene("WinScene");
        } else {
            SceneManager.LoadScene(sceneNames[sceneIndex+1]);
        }
    }

    public void ExitBullet() {
        scaler.Scale(false);
        EvalutePattern.instance.StopAllPatterns();
        StartCoroutine(MoveIn());
    }

}
