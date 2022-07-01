using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonBlock : MonoBehaviour
{
    public delegate void ButtonPressHandler(int num);
    public static event ButtonPressHandler ButtonPress;

    public StoredValue buttonNum;

    private SpriteRenderer sr;

    private bool canPress;
    private IEnumerator disablePress;

    private void DisableSelf()
    {
        canPress = false;
        if (disablePress != null)
        {
            StopCoroutine(disablePress);
        }


        BulletInfiltrate.Exit -= DisableSelf;
    }
    private void Awake()
    {
        buttonNum.value = 0;
        canPress = true;

        sr = GetComponent<SpriteRenderer>();

        BulletInfiltrate.Exit += DisableSelf;
    }

    public IEnumerator Disable()
    {
        canPress = false;
        sr.DOColor(Color.gray, .25f);
        yield return new WaitForSeconds(3f);
        sr.DOColor(Color.white, .25f);

        canPress = true;
    }

    public void Switch()
    {

        disablePress = Disable();
        StartCoroutine(disablePress);

        transform.DOScale(Vector3.one * 1.2f, .25f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);

        buttonNum.value = (buttonNum.value == 0) ? 1 : 0;
        ButtonPress.Invoke((int)buttonNum.value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canPress)
        {
            Switch();
        }
    }
}
