using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoad : MonoBehaviour
{
    public string scene;

    public void LoadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }
}
