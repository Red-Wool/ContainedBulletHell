using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    private int hp = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet")
        {
            hp--;
            if (hp <= 0 && transform.parent.lossyScale.x > .9f)
            {
                SoundManager.instance.explosion.Play();
                ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
