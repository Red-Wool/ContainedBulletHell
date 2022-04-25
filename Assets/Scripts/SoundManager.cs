using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
