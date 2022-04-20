using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float lifeTimer;
    private int bulletID;
    private bool weakened;
    private int activeScene;

    public void SetUp(int id, float setSpeed, int scene)
    {
        lifeTimer = 0f;
        weakened = false;

        speed = setSpeed;
        bulletID = id;
        activeScene = scene;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScenePause.instance.activeScene == activeScene)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifetime)
            {
                gameObject.SetActive(false);
            }

            //Debug.Log(transform.right + " " + transform.right * speed);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
    }

    public void Weaken()
    {
        weakened = BulletInfiltrate.instance.CheckBullet(transform, bulletID);
        speed *= 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMove>().Damage(1);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
