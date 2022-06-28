using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public GameObject cursor;
    public Animator anim;

    private Vector3 temp;
    private Camera cam;

    private KeyCode shoot = KeyCode.Mouse0;

    private void SetControl(OptionObject option)
    {
        shoot = option.controls.GetControl("Shoot");
    }

    private void Awake()
    {
        Cursor.visible = false;
        cam = Camera.main;

        OptionMenu.ChangeControls += SetControl;
    }

    // Update is called once per frame
    void Update()
    {
        temp = cam.ScreenToWorldPoint(Input.mousePosition);
        temp.z = -5;
        cursor.transform.position = temp;

        if (Input.GetKeyDown(shoot))
        {
            anim.SetBool("Shoot",true);
            Cursor.visible = false;
        }
        else if (Input.GetKeyUp(shoot))
        {
            anim.SetBool("Shoot", false);
        }
    }
}
