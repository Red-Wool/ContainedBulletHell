using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiltrateStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        BulletInfiltrate.instance.scaler = this;
        Scale(true);
    }

    public void Scale(bool flag)
    {
        transform.DOScale(flag ? Vector3.one : Vector3.zero, 5f).SetEase(Ease.OutCubic);
    }
}
