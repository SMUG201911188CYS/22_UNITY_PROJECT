using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    public Camera main_camera;

    float moveSpeed = 20.0f;


    void Start()
    {

    }

    void Update()
    {
        if (main_camera.transform.position.z >= -30)
            moveSpeed = 0;

        main_camera.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        moveSpeed += 0.5f;
    }

    private void OnEnable()
    {
       // while(true)
            //this.logo_camera.transform.position = new Vector3(logo_camera.transform.position.x, logo_camera.transform.position.y, logo_camera.transform.position.z * moveSpeed);
    }


}
