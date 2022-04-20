using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Vector2.Lerp(transform.position, ScenePause.instance.activeScene == 1 ? player.transform.position : Vector3.zero, 0.1f);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
