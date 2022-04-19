using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BulletInfiltrate : MonoBehaviour
{
    public static BulletInfiltrate instance { get; private set; }

    public GameObject portal;

    private BulletWeaken[] weakBullets;

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
        weakBullets = weak;
    }

    public bool CheckBullet(Transform obj, int id)
    {
        for (int i = 0; i < weakBullets.Length; i++)
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

            parent.gameObject.SetActive(false);
        }
    }

    public void EnterBullet(string sceneName)
    {
        SpawnPortal(transform, false);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
