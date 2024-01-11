using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class end6_controller : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;

    // Start is called before the first frame update
    void Start()
    {
        image2.SetActive(false);
        Invoke("Image2_active", 1.5f);
        Invoke("Game_exit", 3f);
    }

    void Image2_active()
    {
        Destroy(image1);
        image2.SetActive(true);

    }

    void Game_exit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
