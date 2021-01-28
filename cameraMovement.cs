using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Camera camera;
    private float zoom, scroll, horizontal, vertical;
    void Start()
    {
        camera = Camera.main;
        zoom = camera.orthographicSize;
    }

    //used to move camera
    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom = zoom - scroll;
        camera.orthographicSize = zoom*3;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal + vertical != 0)
            this.transform.Translate(new Vector3(horizontal, vertical) *Time.deltaTime*5);
    }
}
