using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    public float strength;

    private Vector3 temp;
    private void DisableSelf()
    {
        if (gameObject != null)
            gameObject.SetActive(false);
        BulletInfiltrate.Exit -= DisableSelf;
    }
    private void Awake()
    {
        BulletInfiltrate.Exit += DisableSelf;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            temp = (collision.gameObject.transform.position - transform.position).normalized * strength;
            collision.gameObject.GetComponent<PlayerMove>().additionalVelocity = temp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().additionalVelocity = Vector3.zero;
        }
    }
}
