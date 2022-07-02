using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    private int hp = 5;
    private bool canBreak = true;
    private void DisableSelf()
    {
        canBreak = false;
        BulletInfiltrate.Exit -= DisableSelf;
    }
    private void Awake()
    {
        canBreak = true;
        BulletInfiltrate.Exit += DisableSelf;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet")
        {
            hp--;
            if (hp <= 0 && canBreak)
            {
                //SoundManager.instance.graze.Play();
                ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
