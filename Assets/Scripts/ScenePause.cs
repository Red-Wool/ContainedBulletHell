using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePause : MonoBehaviour
{
    public static ScenePause instance { get; private set; }

    public int activeScene;

    private void Awake()
    {
        activeScene = 0;
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
