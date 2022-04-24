using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrateBulletScript : MonoBehaviour {
    public float lifetime;
    public Vector3 moveVector;
    public bool running = false;

    void Start() {

    }

    void Update() {
        transform.position += moveVector * Time.deltaTime;
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) {
            Destroy(gameObject);
        }
    }

    public void SetUp(Vector3 moveVector, float lifetime) {
        this.moveVector = moveVector;
        this.lifetime = lifetime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerMove>().Damage(1);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Border")) {
            Destroy(gameObject);
        }
    }
}