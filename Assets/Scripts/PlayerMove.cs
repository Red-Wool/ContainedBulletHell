using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StoredValue hp;

    [SerializeField] private PlayerShoot shoot;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        hp.value = 4;
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
        if (shoot.canControl)
        {
            hp.value -= damage;
            SoundManager.instance.hurt.Play();
            StartCoroutine(Invincible(3f));
        }
    }

    public IEnumerator Invincible(float time)
    {
        UtilFunctions.PulseObject(shoot.gameObject, 0.6f, 0.1f, 1f, (int)(time / 0.3f));
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -36, 36), Mathf.Clamp(transform.position.y, -20.5f, 20.5f));
        GetComponent<BoxCollider2D>().enabled = true;
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
        }
        else
        {
            hp.value = Mathf.Min(hp.value + 1, 4);
            StartCoroutine(BecomeActive(pos));
        }
    }
    
    IEnumerator BecomeActive(Vector3 pos)
    {
        transform.DOKill();
        //transform.localPosition = Vector3.zero;
        transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutCubic);
        transform.DOMove(pos, 3f).SetEase(Ease.OutElastic);

        if (BulletInfiltrate.instance.CheckGetAll() && !BulletInfiltrate.instance.boss.Final)
        {
            shoot.Second();
            yield return new WaitForSeconds(4f);
        }
        yield return new WaitForSeconds(3f);
        
        shoot.canControl = true;
        shoot.transform.localPosition = Vector3.zero;
        StartCoroutine(Invincible(1.5f));
    }
}
