using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiltrateStart : MonoBehaviour
{
    [SerializeField] private GameObject endGoal;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        transform.position = BulletInfiltrate.instance.lastPos;
        BulletInfiltrate.instance.scaler = this;
        Scale(true);
    }

    public void Scale(bool flag)
    {
        StartCoroutine(Scaling(flag));
    }

    public IEnumerator Scaling(bool flag)
    {
        endGoal.GetComponent<BoxCollider2D>().enabled = false;
        transform.DOScale(flag ? Vector3.one : Vector3.zero, 5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(5f);
        endGoal.GetComponent<BoxCollider2D>().enabled = true;
    }
}
