using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalutePattern : MonoBehaviour
{
    public static EvalutePattern instance { get; private set; }

    [SerializeField] private BossShoot boss; public BossShoot Boss { get { return boss; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("Exist");
            instance = this;
        }
    }

    public void EvaluteBulletPattern(EnemyBulletPattern pattern, Vector2 pos, float angle, float extraSpeed)
    {
        int key = pattern.key;
        for (int i = 0; i < pattern.bulletAngles.Length; i++)
        {
            GameObject obj = boss.BulletPools[key].GetObject();
            obj.transform.parent = boss.Container.transform;

            obj.transform.position = transform.position + new Vector3(pos.x, pos.y, 0f);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + pattern.bulletAngles[i]));
            EnemyBullet bullet = obj.GetComponent<EnemyBullet>();

            bullet.SetUp(key, pattern.speed + extraSpeed, boss.ActiveScene);
        }
    }

    public void EvaluteBulletSequence(EnemyBulletSequence sequence, Vector2 pos, Transform target)
    {
        StartCoroutine(PlayBulletSequence(sequence, pos, target));
    }

    public IEnumerator PlayBulletSequence(EnemyBulletSequence sequence, Vector2 pos, Transform target)
    {
        float angle, anglePlus = 0, speedPlus = 0;

        for (int i = 0; i < sequence.loops; i++)
        {
            angle = (sequence.aimAtPlayer) ? UtilFunctions.AngleTowards(pos, target.position) : 0f;

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
    }
}