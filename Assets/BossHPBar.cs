using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private BossShoot boss;
    [SerializeField] private StoredValue hp;

    [SerializeField] private Image bossHPbar;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossHPbar.fillAmount = Mathf.Lerp(bossHPbar.fillAmount, Mathf.Clamp(hp.value, 0f, boss.MaxHP) / boss.MaxHP, Time.deltaTime * speed);
    }
}
