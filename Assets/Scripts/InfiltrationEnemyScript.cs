using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiltrationEnemyScript : MonoBehaviour {
    [SerializeField] private EnemyBulletSequence bullets;
    [SerializeField] private EnemyMoveSequence move;

    private IEnumerator shooting;

    public float shootRate;
    public float moveRate;
    public int hp;
    public bool look;

    private GameObject player;
    private float bdc = 0;
    private float moveTimer = 0f;

    void Start() {
        player = BulletInfiltrate.instance.player.gameObject;
    }

    void Update() {

        if (look)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        bdc += Time.deltaTime;
        moveTimer += Time.deltaTime;

        // check if it's time to spawn a new bullet
        if (UtilFunctions.CheckTimer(ref bdc, shootRate))
        {
            shooting = EvalutePattern.instance.EvaluteBulletSequence(bullets, transform, player.transform, transform.parent);
            StartCoroutine(shooting);
        }
        if (UtilFunctions.CheckTimer(ref moveTimer, moveRate))
        {
            EvalutePattern.instance.EvaluteMoveSequence(transform, move);
        }
    }

    public bool Damage() {
        hp--;
        if (hp <= 0) {
            // maybe we can add a cool effect here, or require the player to kill all the enemies before getting the star?
            transform.DOKill();
            if (shooting != null)
                StopCoroutine(shooting);

            SoundManager.instance.explosion.Play();
            ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, transform.position);
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}