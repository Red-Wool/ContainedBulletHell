using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GotoScene : MonoBehaviour {
    public void LoadScene(string targetScene) {
        DOTween.KillAll();
        SceneManager.LoadScene(targetScene);
    }
}
