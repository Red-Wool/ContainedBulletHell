using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float lifeTimer;
    private int bulletID;
    private bool weakened;
    private int activeScene;

    [SerializeField] private BulletModify mod;

    private BoxCollider2D hitbox;

    public void SetUp(int id, float setSpeed, int scene)
    {
        UtilFunctions.GrowObject(gameObject);

        lifeTimer = 0f;
        weakened = false;

        if (mod.randomAngle)
        {
            mod.angleUp = Random.Range(-mod.angleRange, mod.angleRange);
        }

        speed = setSpeed;
        bulletID = id;
        activeScene = scene;
        transform.localScale = Vector3.one;
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScenePause.instance.activeScene == activeScene)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifetime)
            {
                Death();
            }

            speed += mod.speedUp * Time.deltaTime;
            transform.eulerAngles += Vector3.forward * mod.angleUp * Time.deltaTime;

            //Debug.Log(transform.right + " " + transform.right * speed);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        hitbox.enabled = ScenePause.instance.activeScene == activeScene;


    }

    public void Weaken()
    {
        weakened = BulletInfiltrate.instance.CheckBullet(transform, bulletID);
        speed *= 0.3f;
    }

    public void Death()
    {
        if (mod.spawnOnDeath)
        {
            EvalutePattern.instance.EvaluteBulletSequence(mod.bulletOnDeath, transform, EvalutePattern.instance.Boss.player.transform);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMove>().Damage(1);
            Death();
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            Death();
        }
    }
}
