using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    [Header("Stored Values"), SerializeField] private StoredValue hp;
    [SerializeField] private StoredValue intel;

    [Space(10), Header("Cost Reference")]
    [SerializeField] private PlayerShoot laserCost;

    [Space(10), Header("Image Reference")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image intelBar;

    [Space(10), Header("Config")]
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hp.value / 4f, Time.deltaTime * speed);
        intelBar.fillAmount = Mathf.Lerp(intelBar.fillAmount, Mathf.Clamp(intel.value,0f,laserCost.LaserCost) / laserCost.LaserCost, Time.deltaTime * speed);
    }
}
