using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StoredValue hp;

    [SerializeField] private PlayerShoot shoot;
    [SerializeField] private GameObject sprite;

    private bool invincible;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
        hp.value = 8;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            move *= 0.5f;
        }
        
        if (!shoot.canControl)
        {
            move *= 0f;
        }

        rb.velocity = (move * speed);
    }

    public void Damage(int damage)
    {
        if (shoot.canControl && !invincible)
        {
            hp.value -= damage;
            if (hp.value <= 0)
            {
                StartCoroutine(Death());

            }
            else
            {
                SoundManager.instance.hurt.Play();
                StartCoroutine(Invincible(3f));
            }
        }
    }

    public IEnumerator Death()
    {
        SoundManager.instance.explosion.Play();
        ParticleManager.instance.PlayParticle(ParticleManager.instance.explosion, transform.position);
        shoot.canControl = false;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Invincible(float time)
    {
        UtilFunctions.PulseObject(sprite, 0.6f, 0.1f, 1f, (int)(time / 0.3f));
        invincible = true;
        yield return new WaitForSeconds(time);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -36, 36), Mathf.Clamp(transform.position.y, -20.5f, 20.5f));
        invincible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit");
    }

    public void InfiltrateTransition(Vector3 pos, bool flag)
    {
        if (flag)
        {
            shoot.canControl = false;
            transform.DOScale(Vector3.zero, 2f).SetEase(Ease.OutCubic);
            transform.DOMove(pos, 3f).SetEase(Ease.OutElastic).OnComplete(() => transform.DOMove(Vector3.zero, 8f));
            ParticleManager.instance.PlayParticle(ParticleManager.instance.infiltrateEnter, pos);
        }
        else
        {
            
            ParticleManager.instance.PlayParticle(ParticleManager.instance.infiltrateExit, pos);
            StartCoroutine(BecomeActive(pos));
        }
    }

    public void Heal(int num)
    {
        hp.value = Mathf.Min(hp.value + num, 8);
        ParticleManager.instance.hp.Play();
    }
    
    IEnumerator BecomeActive(Vector3 pos)
    {
        transform.DOKill();
        //transform.localPosition = Vector3.zero;
        transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutCubic);
        transform.DOMove(pos, 3f).SetEase(Ease.OutElastic);

        Heal(2);

        
        if (BulletInfiltrate.instance.CheckGetAll() && !BulletInfiltrate.instance.boss.Final)
        {
            shoot.Second();
            yield return new WaitForSeconds(4f);
            ParticleManager.instance.PlayParticle(ParticleManager.instance.recieveNew, transform.position);
        }
        yield return new WaitForSeconds(3f);
        
        shoot.canControl = true;
        shoot.transform.localPosition = Vector3.zero;
        StartCoroutine(Invincible(1.5f));
    }
}
