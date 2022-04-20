using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private ObjectPool normalShoot;
    [SerializeField] private ObjectPool weakLaser;

    [SerializeField] private float shootRate;
    private float rateTimer;
    public bool canControl { get; set; }

    [SerializeField] private float laserCost; public float LaserCost { get { return laserCost; } }
    [SerializeField] private StoredValue intel;
    [SerializeField] private GameObject graze;
    private Camera cam;

    [SerializeField] private PlayerMove move;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        canControl = true;
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

        if (canControl)
        {
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
        
    }

    public void Shoot(GameObject obj, float angle, PlayerWeapon type)
    {
        rateTimer = 0f;

        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        obj.GetComponent<PlayerBullet>().SetUp(intel, type, ScenePause.instance.activeScene);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Gaming!");
            intel.value += 6;

            UtilFunctions.PulseObject(graze, 0.2f, 0.5f, 0f, 1);

        }
        else if (collision.gameObject.CompareTag("Portal"))
        {
            BulletInfiltrate.instance.EnterBullet(collision.gameObject.GetComponent<InfiltratePortal>().scene);

            move.InfiltrateTransition(collision.gameObject.transform.position, true);
        }
        else if (collision.gameObject.CompareTag("Exit"))
        {
            collision.gameObject.SetActive(false);
            BulletInfiltrate.instance.ExitBullet();

            move.InfiltrateTransition(collision.gameObject.transform.position, true);
        }
    }

    
}

public enum PlayerWeapon
{
    Intel,
    Weaken,
    Damage
}
