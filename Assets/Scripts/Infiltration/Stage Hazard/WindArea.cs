using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public Vector2 windDirection;

    private void DisableSelf()
    {
        if (gameObject != null)
        {
            BulletInfiltrate.Exit -= DisableSelf;
            gameObject.SetActive(false);
        }
            
            
    }
    private void Awake()
    {
        BulletInfiltrate.Exit += DisableSelf;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().additionalVelocity = windDirection;
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
