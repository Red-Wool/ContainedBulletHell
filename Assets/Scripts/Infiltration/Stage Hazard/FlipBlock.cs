using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class FlipBlock : MonoBehaviour
{
    public int onNum;

    private BoxCollider2D hitbox;
    private SpriteRenderer sr;

    private bool insta;

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        Flip(0);
        ButtonBlock.ButtonPress += Flip;
    }

    private void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    public void Flip(int num)
    {
        if (hitbox)
        {
            if (num == onNum)
            {
                hitbox.enabled = true;
                StartCoroutine(Instakill());
                transform.DOScale(Vector3.one * 4f, .25f).SetEase(Ease.InOutSine);
                sr.DOFade(1f, .25f);
            }
            else
            {
                hitbox.enabled = false;
                transform.DOScale(Vector3.one, .25f).SetEase(Ease.InOutSine);
                sr.DOFade(.5f, .25f);
            }
        }
    }

    public IEnumerator Instakill()
    {
        insta = true;
        yield return new WaitForSeconds(.25f);
        insta = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && insta)
        {
            collision.gameObject.GetComponent<PlayerMove>().Damage(8);
        }
    }
}
