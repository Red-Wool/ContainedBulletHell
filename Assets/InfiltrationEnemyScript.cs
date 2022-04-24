using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationEnemyScript : MonoBehaviour {
    public float speed = 5;
    public float bulletSpeed = 8;
    public float bulletLifetime = 10;
    public float bulletDelay = 2;

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
            InfiltrateBulletScript b = Instantiate(bulletPrefab).GetComponent<InfiltrateBulletScript>();
            b.SetUp(Vector3.MoveTowards(transform.position, player.transform.position).normalized * bulletSpeed, bulletLifetime);
        }
    }
}