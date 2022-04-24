using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentBar : MonoBehaviour
{
    [SerializeField] private Image[] image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetAlpha(0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetAlpha(1f);
        }
    }

    private void SetAlpha(float val)
    {
        Color col;

        foreach(Image img in image)
        {
            col = img.color;
            col.a = val;

            img.color = col;
        }
    }
}
