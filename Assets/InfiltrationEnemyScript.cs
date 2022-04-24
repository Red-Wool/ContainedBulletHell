using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationEnemyScript : MonoBehaviour {
    public float speed = 5;
    public float bulletSpeed = 17;
    public float bulletLifetime = 5;
    public float bulletDelay = 0.8f;
    public float hp = 10;

    public GameObject bulletPrefab;
    private GameObject player;
    private float bdc;

    void Start() {
        bdc = bulletDelay;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        bdc -= Time.deltaTime;

        // check if it's time to spawn a new bullet
        if (bdc <= 0) {
            bdc = bulletDelay;
            Vector3 bulletMovement = (player.transform.position - transform.position).normalized;
            Vector3 offsetSpawnPoint = Vector3.MoveTowards(transform.position, player.transform.position, 3);

            InfiltrateBulletScript b = Instantiate(bulletPrefab, offsetSpawnPoint, Quaternion.identity).GetComponent<InfiltrateBulletScript>();
            b.SetUp((bulletMovement*bulletSpeed), bulletLifetime);
        }
    }

    public void Damage() {
        hp--;
        if (hp <= 0) {
            // maybe we can add a cool effect here, or require the player to kill all the enemies before getting the star?
            Destroy(gameObject);
        }
    }
}