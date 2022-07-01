using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastBlock : MonoBehaviour
{
    public EnemyBulletSequence shrapnel;

    private bool canBreak = true;
    private void DisableSelf()
    {
        canBreak = false;
        BulletInfiltrate.Exit -= DisableSelf;
    }
    private void Awake()
    {
        canBreak = false;

        StartCoroutine(DisableForABit());

        BulletInfiltrate.Exit += DisableSelf;
    }

    public IEnumerator DisableForABit()
    {
        canBreak = false;
        yield return new WaitForSeconds(3f);
        canBreak = true;
    }

    public void Explode()
    {
        //SoundManager.instance.explosion.Play();
        ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, transform.position);
        SoundManager.instance.explosion.Play();
        StartCoroutine(EvalutePattern.instance.EvaluteBulletSequence(shrapnel, transform, transform, transform.parent));
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canBreak)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canBreak)
        {
            Explode();
        }
    }
}
