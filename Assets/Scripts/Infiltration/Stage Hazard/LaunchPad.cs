using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public GameObject target;
    private void DisableSelf()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    private void Awake()
    {
        BulletInfiltrate.Exit += DisableSelf;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().Launch(target.transform.position);
        }
    }
}
