using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BulletInfiltrate : MonoBehaviour
{
    public static BulletInfiltrate instance { get; private set; }

    public PlayerMove player;
    public BossShoot boss;
    public GameObject portal;
    public GameObject container;

    private List<BulletWeaken> weakBullets;
    private string bulletSceneName;
    public Vector3 lastPos { get; private set; }

    [HideInInspector] public InfiltrateStart scaler;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("Exist");
            instance = this;
        }
    }

    public void AddWeakenBullet(BulletWeaken[] weak)
    {
        weakBullets = new List<BulletWeaken>();
        foreach(BulletWeaken i in weak)
        {
            weakBullets.Add(i);
        }
    }

    public bool CheckGetAll()
    {
        if (weakBullets.Count == 0)
        {
            boss.Second();
            return true;
        }
        return false;
    }

    public void Victory()
    {
        for (int i = 0; i < weakBullets.Count; i++)
        {
            if (weakBullets[i].sceneName == bulletSceneName)
            {
                weakBullets.Remove(weakBullets[i]);
            }
        }
    }

    public bool CheckBullet(Transform obj, int id)
    {
        for (int i = 0; i < weakBullets.Count; i++)
        {
            if (id == weakBullets[i].weakID)
            {
                ActivateBulletInfiltration(obj, weakBullets[i].sceneName);
                return true;
            }
        }
        return false;
    }

    public void ActivateBulletInfiltration(Transform obj, string sceneName)
    {
        Debug.Log(sceneName + " Activated111!!");
        portal.GetComponent<InfiltratePortal>().scene = sceneName;
        SpawnPortal(obj, true);
    }

    public void SpawnPortal(Transform obj, bool flag)
    {
        if (flag)
        {
            portal.GetComponent<BoxCollider2D>().enabled = true;
            portal.transform.position = obj.position;
            portal.transform.parent = obj;
            portal.transform.localScale = Vector3.zero;
            portal.transform.DOScale(Vector3.one, 1f);
        }
        else
        {
            Transform parent = portal.transform.parent;

            portal.GetComponent<BoxCollider2D>().enabled = false;
            portal.transform.parent = null;
            portal.transform.DOScale(Vector3.zero, 1f);
            lastPos = parent.transform.position;

            parent.gameObject.SetActive(false);
        }
    }

    public void EnterBullet(string sceneName)
    {
        SpawnPortal(transform, false);
        ScenePause.instance.activeScene = 1;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        StartCoroutine(MoveAway());

        bulletSceneName = sceneName;
    }

    public IEnumerator MoveAway()
    {
        container.transform.DOScale(Vector3.one * 10f, 5f);
        //container.transform.DOMove(Vector3.down * 100, 5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(5f);
        player.InfiltrateTransition(lastPos, false);
        SoundManager.instance.infiltrateOut.Play();
        container.transform.position = Vector3.down * 100000;
    }

    public IEnumerator MoveIn()
    {
        //container.transform.position = Vector3.down * 100;
        container.transform.position = Vector3.zero;
        container.transform.DOScale(Vector3.one, 2f);
        yield return new WaitForSeconds(2f);
        SoundManager.instance.infiltrateOut.Play();
        player.InfiltrateTransition(lastPos, false);
        yield return new WaitForSeconds(3f);
        ScenePause.instance.activeScene = 0;
        //SceneManager.UnloadSceneAsync(bulletSceneName);

    }

    public void ExitBullet()
    {
        scaler.Scale(false);
        StartCoroutine(MoveIn());
    }

}
