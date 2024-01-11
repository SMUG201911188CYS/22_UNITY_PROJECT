using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneLoader_Enter : MonoBehaviour
{
    public GameObject fade_out;

    // Start is called before the first frame update
    void Start()
    {
        fade_out.GetComponent<Image>();
    }

    void Load_next_scene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }


    void OnEnable()
    {
        fade_out.SetActive(true);
        Invoke("Load_next_scene", 2f);
    }
}
