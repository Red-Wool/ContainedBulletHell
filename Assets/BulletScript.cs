using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float lifeTimer;
    private PlayerWeapon type;
    private StoredValue intel;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifetime)
        {
            gameObject.SetActive(false);
        }

        //Debug.Log(transform.right + " " + transform.right * speed);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetUp(StoredValue val, PlayerWeapon bullet)
    {
        intel = val;
        lifeTimer = 0f;
        type = bullet;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (type)
            {
                case PlayerWeapon.Intel:
                    intel.value += 5f;
                    break;
                case PlayerWeapon.Weaken:

                    break;
            }
            
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
