using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private BossBulletData bulletData;
    [SerializeField] private BossBulletData secondData;

    [SerializeField] private StoredValue bossHP;
    [SerializeField] private float maxHP; public float MaxHP { get { return maxHP; } }

    private IEnumerator shooting;

    private int sessionPointer;
    private int sequencePointer;
    private bool second; public bool Phase2 { get { return second; } }
    private bool final; public bool Final { get { return final; } }
    public BulletWeaken finalScene;

    private float timer;

    [SerializeField] private ObjectPool[] bulletPools; public ObjectPool[] BulletPools { get { return bulletPools; } }
    [SerializeField] private BulletWeaken[] weakBullets;
    [SerializeField] private GameObject container; public GameObject Container { get { return container; } }

    [SerializeField] private int activeScene; public int ActiveScene { get { return activeScene; } }

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        second = false;
        final = false;

        CheckBulletPool();
        bossHP.value = maxHP;
        sessionPointer = Random.Range(0, bulletData.Session.Length - 1);
        sequencePointer = 0;

        //Debug.Log(weakBullets[0].sceneName);
        BulletInfiltrate.instance.AddWeakenBullet(weakBullets);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScenePause.instance.activeScene == activeScene)
        {
            if (bossHP.value <= 0)
            {
                final = true;
            }
            else
            {
                timer += Time.deltaTime;
            }

            EnemyBulletSession curSession = second ? secondData.Session[sessionPointer] : bulletData.Session[sessionPointer];

            if (sequencePointer == curSession.sequence.Length)
            {
                if (CheckTimer(curSession.coolDown))
                {
                    int[] next = curSession.nextSessions;

                    sessionPointer = next[Random.Range(0, next.Length)];
                    sequencePointer = 0;
                }
            }
            else
            {
                EnemyBulletSequence curSequence = curSession.sequence[sequencePointer];

                if (CheckTimer(curSequence.wait))
                {
                    shooting = EvalutePattern.instance.EvaluteBulletSequence(curSequence, transform, player.transform, curSession.spawnPos, container.transform);
                    StartCoroutine(shooting);

                    sequencePointer++;
                }
            }
        }
    }

    public void Damage(float damage)
    {
        bossHP.value -= damage;
        if (!final && bossHP.value <= 0)
        {
            ParticleManager.instance.Toggle(ParticleManager.instance.stars, true);
            StopCoroutine(shooting);
            final = true;
        }
    }

    public void Second()
    {
        second = true;
        sessionPointer = Random.Range(0, secondData.Session.Length - 1);
        sequencePointer = 0;
    }

    public void CheckBulletPool()
    {
        for (int i = 0; i < bulletPools.Length; i++)
        {
            bulletPools[i].AddObjects();
        }
    }

    public bool CheckTimer(float time)
    {
        if (timer > time)
        {
            timer -= time;
            return true;
        }
        return false;
    }

    /*public void EvaluteBulletPattern(EnemyBulletPattern pattern, Vector2 pos, float angle, float extraSpeed)
    {
        int key = pattern.key;
        for (int i = 0; i < pattern.bulletAngles.Length; i++)
        {
            GameObject obj = bulletPools[key].GetObject();
            obj.transform.parent = container.transform;

            obj.transform.position = transform.position + new Vector3(pos.x, pos.y, 0f);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + pattern.bulletAngles[i]));
            EnemyBullet bullet = obj.GetComponent<EnemyBullet>();

            bullet.SetUp(key, pattern.speed + extraSpeed, activeScene);
        }
    }

    public float AngleTowards(Vector3 pos, Vector3 target)
    {
        Vector3 dir = target - pos;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public IEnumerator EvaluteBulletSequence(EnemyBulletSequence sequence, Vector2 pos)
    {
        float angle, anglePlus = 0, speedPlus = 0;

        for (int i = 0; i < sequence.loops; i++)
        {
            angle = (sequence.aimAtPlayer) ? AngleTowards(pos + new Vector2(transform.position.x, transform.position.y), player.transform.position) : 0f;

            angle += Random.Range(-sequence.angleOffset, sequence.angleOffset);
            angle += anglePlus;

            EvaluteBulletPattern(sequence.pattern, pos, angle, speedPlus);
            if (sequence.rate > 0f)
            {
                yield return new WaitForSeconds(sequence.rate);
            }

            anglePlus += sequence.loopAngle;
            speedPlus += sequence.loopSpeed;
        }
    }*/
}

[System.Serializable]
public class BulletWeaken
{
    public int weakID;
    public string sceneName;
    public GameObject display;
    public string enterText;
    [TextArea]
    public string subText;
}
