using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private BossShoot boss;
    [SerializeField] private StoredValue hp;

    [SerializeField] private GameObject[] displays;
    [SerializeField] private GameObject final;
    [SerializeField] private Image bossHPbar;
    private Vector3 savedPos;

    private Camera cam;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        savedPos = bossHPbar.transform.position;
        bossHPbar.transform.parent.position += Vector3.up * 10;
    }

    // Update is called once per frame
    void Update()
    {
        bossHPbar.fillAmount = Mathf.Lerp(bossHPbar.fillAmount, Mathf.Clamp(hp.value, 0f, boss.MaxHP) / boss.MaxHP, Time.deltaTime * speed);
        
    }

    public void ChangeBar()
    {
        StartCoroutine(BarChange());
    }

    public IEnumerator BarChange()
    {
        BulletInfiltrate.instance.player.Heal(8);
        savedPos += cam.gameObject.transform.position;
        for (int i = 0; i < displays.Length; i++)
        {
            displays[i].transform.DOMove(savedPos, 3f).SetEase(Ease.InOutQuad);
            displays[i].transform.DOScale(Vector3.zero, 2.5f).SetEase(Ease.OutCubic);
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < displays.Length; i++)
        {
            displays[i].SetActive(false);
        }
        final.SetActive(true);
        final.transform.localScale = Vector3.zero;
        final.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutCubic);
        final.transform.DOMove(final.transform.position + Vector3.up * 1f, 1f).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(1f);

        final.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.OutCubic);
        final.transform.DOMove(BulletInfiltrate.instance.player.transform.position, 1f).SetEase(Ease.OutCirc);

        yield return new WaitForSeconds(3f);

        bossHPbar.transform.parent.DOMove( savedPos, 1f).SetEase(Ease.InOutQuad);
        //yield return new WaitForSeconds(1f);
        //bossHPbar.transform.parent.DOMove(savedPos, 1f).SetEase(Ease.InOutQuad);
    }
}
