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

    public Sprite redButton;
    public Sprite blueButton;

    private SpriteRenderer sr;

    private bool canPress;
    private IEnumerator disablePress;

    public static void ResetButtonPress()
    {
        ButtonPress = null;
    }

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
        ButtonPress += UpdateSprite;

        BulletInfiltrate.Exit += DisableSelf;
    }

    public IEnumerator Disable()
    {
        canPress = false;
        sr.DOColor(Color.gray, .25f);
        yield return new WaitForSeconds(1f);
        sr.DOColor(Color.white, .25f);

        canPress = true;
    }

    public void UpdateSprite(int num)
    {
        sr.sprite = (num == 0) ? redButton : blueButton;
        disablePress = Disable();
        StartCoroutine(disablePress);
    }

    public void Switch()
    {

        

        transform.DOScale(Vector3.one * 5f, .15f).SetEase(Ease.InOutCirc).SetLoops(2, LoopType.Yoyo);

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
