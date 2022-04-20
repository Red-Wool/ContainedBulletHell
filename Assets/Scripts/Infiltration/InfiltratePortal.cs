using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltratePortal : MonoBehaviour
{
    public string scene;

    private void Update()
    {
        if (transform.parent && !transform.parent.gameObject.activeSelf)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent = null;
        }
    }
}
