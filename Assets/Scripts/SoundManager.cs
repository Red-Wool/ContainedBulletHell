using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public AudioSource music;
    public AudioSource shoot;
    public AudioSource laser;
    public AudioSource laserGet;
    public AudioSource graze;
    public AudioSource hurt;
    public AudioSource explosion;
    public AudioSource infiltrateIn;
    public AudioSource infiltrateOut;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void PauseMusic(bool flag)
    {
        //very unelegant but idk how to do other way for now
        if (flag)
        {
            music.Pause();
            shoot.Pause();
            laser.Pause();
            laserGet.Pause();
            graze.Pause();
            hurt.Pause();
            explosion.Pause();
            infiltrateIn.Pause();
            infiltrateOut.Pause();
        }
        else
        {
            music.UnPause();
            shoot.UnPause();
            laser.UnPause();
            laserGet.UnPause();
            graze.UnPause();
            hurt.UnPause();
            explosion.UnPause();
            infiltrateIn.UnPause();
            infiltrateOut.UnPause();
        }
        
    }
}
