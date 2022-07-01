using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public GameObject target;
    private void DisableSelf()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        BulletInfiltrate.Exit -= DisableSelf;
    }
    private void Awake()
    {
        BulletInfiltrate.Exit += DisableSelf;

        StartCoroutine(DisableForABit());
    }

    public IEnumerator DisableForABit()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().Launch(target.transform.position);
        }
    }
}
