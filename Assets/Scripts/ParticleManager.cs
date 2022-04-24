using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance { get; private set; }

    public ParticleSystem intelShot;
    public ParticleSystem shoot;
    public ParticleSystem explosionDeath;

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

}
