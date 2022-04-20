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
            UtilFunctions.PulseObject(shoot.gameObject, 0.3f, 0.1f, 1f, 5);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
    }

    public void InfiltrateTransition(Vector3 pos, bool flag)
    {
        if (flag)
        {
            shoot.canControl = false;
            transform.DOScale(Vector3.zero, 2f).SetEase(Ease.OutCubic);
            transform.DOMove(pos, 3f).SetEase(Ease.OutElastic).OnComplete(() => transform.DOMove(Vector3.zero, 10f));
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
        yield return new WaitForSeconds(3f);
        //transform.localPosition = Vector3.zero;
        shoot.canControl = true;
    }
}
