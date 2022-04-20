using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class UtilFunctions
{
    public static void PulseObject(GameObject obj, float time, float iOpacity, float fOpacity, int loops)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.DOKill();
        //sr.DOFade(0.5f, 0.1f).OnComplete(() => sr.DOFade(0f, 0.1f));
        sr.DOFade(iOpacity, time * 0.5f).OnComplete(() => sr.DOFade(fOpacity, time * 0.5f)).SetLoops(loops);
    }
}
