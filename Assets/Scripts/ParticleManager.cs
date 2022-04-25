using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance { get; private set; }

    public ParticleSystem intel;
    public ParticleSystem hp;
    public ParticleSystem shoot;
    public ParticleSystem explosion;
    public ParticleSystem stars;
    public ParticleSystem infiltrateEnter;
    public ParticleSystem infiltrateExit;

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

    public void PlayParticle(ParticleSystem ps, Vector3 pos)
    {
        ps.transform.position = pos;
        ps.Play();
    }

    public void Toggle(ParticleSystem ps, bool flag)
    {
        var main = ps.main;
        main.loop = flag;
        if (flag)
        {
            ps.Play();
        }
    }

}
