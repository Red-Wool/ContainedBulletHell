using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour {
    public float speed;
    public float lifetime;

    public BulletModify mod;

    private float lifeTimer;
    private PlayerWeapon type;
    private StoredValue intel;
    private float speedUp;

    // Start is called before the first frame update
    void Start() {
        lifeTimer = 0f;
        
    }

    // Update is called once per frame
    void Update() {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifetime) {
            gameObject.SetActive(false);
        }

        speedUp += mod.speedUp * Time.deltaTime;
        transform.eulerAngles += Vector3.forward * mod.angleUp * Time.deltaTime;

        transform.Translate(Vector3.right * (speed + speedUp) * Time.deltaTime);
    }

    public void SetUp(StoredValue val, PlayerWeapon bullet) {
        UtilFunctions.GrowObject(gameObject);

        intel = val;
        lifeTimer = 0f;
        type = bullet;

        speedUp = 0;

        if (mod.randomAngle)
        {
            mod.angleUp = Random.Range(-mod.angleRange, mod.angleRange);
        }
    }

    private void CheckGotLaser(float val)
    {
        if (intel.value >= 350 && intel.value - val < 350)
        {
            ParticleManager.instance.Toggle(ParticleManager.instance.intel, true);
            SoundManager.instance.laserGet.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            StatsManager.instance.shotsHit++;
            switch (type) {
                case PlayerWeapon.Intel:
                    if (intel)
                        intel.value += 2f;
                    ParticleManager.instance.PlayParticle(ParticleManager.instance.shootHit, Vector3.Lerp(transform.position, collision.gameObject.transform.position,.2f));
                    CheckGotLaser(2f);
                    break;
                case PlayerWeapon.Damage:
                    if (intel)
                        intel.value += 20f;
                    CheckGotLaser(20f);
                    collision.gameObject.GetComponent<BossShoot>().Damage(5);
                    ParticleManager.instance.PlayParticle(ParticleManager.instance.damageHit, Vector3.Lerp(transform.position, collision.gameObject.transform.position, .1f));
                    break;
                case PlayerWeapon.Weaken:
                    if (collision.GetComponent<BossShoot>().Final) {
                        ParticleManager.instance.PlayParticle(ParticleManager.instance.weakenHit, collision.gameObject.transform.position);
                        BulletInfiltrate.instance.BossPortal();
                    }
                    break;
            }
            gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("InfEnemy")) {
            StatsManager.instance.shotsHit++;
            if (collision.gameObject.GetComponent<InfiltrationEnemyScript>().Damage())
            {
                intel.value += 50f;
                CheckGotLaser(50f);
            }
            ParticleManager.instance.PlayParticle(ParticleManager.instance.damageHit, Vector3.Lerp(transform.position, collision.gameObject.transform.position, .2f));
            gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("Border")) {
            gameObject.SetActive(false);
        } else if (type == PlayerWeapon.Weaken && collision.gameObject.CompareTag("EnemyBullet")) {
            StatsManager.instance.shotsHit++;
            collision.gameObject.GetComponent<EnemyBullet>().Weaken();
            ParticleManager.instance.PlayParticle(ParticleManager.instance.weakenHit, Vector3.Lerp(transform.position, collision.gameObject.transform.position, .8f));
            gameObject.SetActive(false);
        }
    }
}