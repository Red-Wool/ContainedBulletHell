using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private ObjectPool normalShoot;
    [SerializeField] private ObjectPool weakLaser;

    [SerializeField] private float shootRate;
    private float rateTimer;

    [SerializeField] private float laserCost;
    [SerializeField] private StoredValue intel;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        normalShoot.AddObjects();
        weakLaser.AddObjects();

        intel.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = mousePos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rateTimer += Time.deltaTime;


        if (Input.GetMouseButton(0) && rateTimer > shootRate)
        {
            Shoot(normalShoot.GetObject(), angle, PlayerWeapon.Intel);
        }
        else if (Input.GetMouseButtonDown(1) && laserCost <= intel.value)
        {
            intel.value -= laserCost;
            Shoot(weakLaser.GetObject(), angle, PlayerWeapon.Weaken);
        }
    }

    public void Shoot(GameObject obj, float angle, PlayerWeapon type)
    {
        rateTimer = 0f;

        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        obj.GetComponent<BulletScript>().SetUp(intel, type);

    }
}

public enum PlayerWeapon
{
    Intel,
    Weaken,
    Damage
}
