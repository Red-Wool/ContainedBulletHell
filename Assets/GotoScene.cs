﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour {
    public void LoadScene(string targetScene) {
        SceneManager.LoadScene(targetScene);
    }
}
