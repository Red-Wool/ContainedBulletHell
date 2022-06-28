using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            instance = this;
        }
    }

    public void StopAllPatterns()
    {
        StopAllCoroutines();
    }

    public void EvaluteBulletPattern(EnemyBulletPattern pattern, Vector2 pos, float angle, float extraSpeed, Transform parent)
    {
        int key = pattern.key;
        for (int i = 0; i < pattern.bulletAngles.Length; i++)
        {
            GameObject obj = boss.BulletPools[key].GetObject();
            obj.transform.parent = parent;

            obj.transform.position = transform.position + new Vector3(pos.x, pos.y, 0f);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + pattern.bulletAngles[i]));
            EnemyBullet bullet = obj.GetComponent<EnemyBullet>();

            bullet.SetUp(key, pattern.speed + extraSpeed, ScenePause.instance.activeScene);
        }
    }

    public IEnumerator EvaluteBulletSequence(EnemyBulletSequence sequence, Transform pos, Transform target, Transform parent)
    {
        return EvaluteBulletSequence(sequence, pos, target, new Vector2[] { Vector2.zero }, parent);
    }

    public IEnumerator EvaluteBulletSequence(EnemyBulletSequence sequence, Transform pos, Transform target, Vector2[] randPos, Transform parent)
    {
        return PlayBulletSequence(sequence, pos, target, randPos, parent);
    }

    public IEnumerator PlayBulletSequence(EnemyBulletSequence sequence, Transform pos, Transform target, Vector2[] randPos, Transform parent)
    {
        float angle, anglePlus = 0, speedPlus = 0;
        int r = Random.Range(0, randPos.Length);
        Vector3 spawn;

        for (int i = 0; i < sequence.loops; i++)
        {
            if (!pos) { break; }

            spawn = randPos[r] + UtilFunctions.Vec3ToVec2(pos.position);

            angle = (sequence.aimAtPlayer) ? UtilFunctions.AngleTowards(spawn, target.position) : 0f;

            angle += Random.Range(-sequence.angleOffset, sequence.angleOffset) + anglePlus;

            EvaluteBulletPattern(sequence.pattern, spawn, angle, speedPlus, parent);
            if (sequence.rate > 0f)
            {
                yield return new WaitForSeconds(sequence.rate);
            }

            anglePlus += sequence.loopAngle;
            speedPlus += sequence.loopSpeed;
        }
    }

    public void EvaluteMoveSequence(Transform obj, EnemyMoveSequence sequence)
    {
        EnemyMovePattern pattern = sequence.pattern;
        obj.DOLocalMove(pattern.pos[Random.Range(0, pattern.pos.Length)] + UtilFunctions.RandVec2Range(sequence.randomDisplace), pattern.time).SetEase(pattern.easeStyle);
    }
}