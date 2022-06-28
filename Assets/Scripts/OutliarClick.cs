using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutliarClick : MonoBehaviour
{
    public ParticleSystem clickPS;
    
    public void Click()
    {
        clickPS.Play();
        SoundManager.instance.shoot.Play();
    }
}
