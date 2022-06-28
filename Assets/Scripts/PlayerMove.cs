using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private KeyCode moveLeft = KeyCode.A;
    private KeyCode moveRight = KeyCode.D;
    private KeyCode moveUp = KeyCode.W;
    private KeyCode moveDown = KeyCode.S;
    private KeyCode slowMove = KeyCode.LeftShift;

    private bool smToggleCheck;
    private bool smToggleOn;

    [SerializeField] private float speed;
    [SerializeField] private StoredValue hp;

    [SerializeField] private PlayerShoot shoot;
    [SerializeField] private GameObject sprite;

    public Vector3 additionalVelocity;
    private bool invincible;

    private Rigidbody2D rb;

    private void SetControl(OptionObject option)
    {
        moveLeft = option.controls.GetControl("MoveLeft");
        moveRight = option.controls.GetControl("MoveRight");
        moveUp = option.controls.GetControl("MoveUp");
        moveDown = option.controls.GetControl("MoveDown");
        slowMove = option.controls.GetControl("SlowMove");

        smToggleCheck = option.toggleSlowMove;
        smToggleOn = false;
    }
    private void Awake()
    {
        invincible = false;
        hp.value = 8;
        rb = GetComponent<Rigidbody2D>();

        OptionMenu.ChangeControls += SetControl;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int CheckInput(KeyCode key)
    {
        return Input.GetKey(key) ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = CheckInput(moveRight) - CheckInput(moveLeft);//Input.GetAxisRaw("Horizontal");
        float y = CheckInput(moveUp) - CheckInput(moveDown);//Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;

        if (smToggleCheck && Input.GetKeyDown(slowMove))
        {
            smToggleOn = !smToggleOn;
        }
        else if (Input.GetKey(slowMove) || smToggleOn)
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
            StatsManager.instance.damageTaken++;
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

    public void Launch(Vector3 pos)
    {
        StartCoroutine(Launch(pos, 1f));
    }
    public IEnumerator Launch(Vector3 pos, float time)
    {
        shoot.canControl = false;
        transform.DOMove(pos, time);
        transform.DOScale(Vector3.one * 2, time * .5f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);

        yield return new WaitForSeconds(time);

        Invincible(.1f);
        shoot.canControl = true;
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
