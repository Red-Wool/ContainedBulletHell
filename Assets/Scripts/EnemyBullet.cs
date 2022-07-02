using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    public float grazeValue;

    private float lifeTimer;
    private int bulletID;
    private bool weakened;
    private int activeScene;

    private Transform player;

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
        if (mod.chase)
        {
            player = BulletInfiltrate.instance.player.gameObject.transform;
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
                lifeTimer = -10f;
            }

            speed += mod.speedUp * Time.deltaTime;
            transform.eulerAngles += Vector3.forward * mod.angleUp * Time.deltaTime;
            if (mod.chase)
            {
                Vector3 dir = player.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

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

    public IEnumerator Shrink()
    {
        transform.DOScale(Vector3.zero, .5f);
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }

    public void Death()
    {
        
        if (mod.spawnOnDeath && gameObject.activeSelf)
        {
            StartCoroutine(EvalutePattern.instance.EvaluteBulletSequence(mod.bulletOnDeath, transform, BulletInfiltrate.instance.player.transform, transform.parent));
        }
        if (mod.chase)
        {
            speed = 0;
            StartCoroutine(Shrink());
            return;
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
