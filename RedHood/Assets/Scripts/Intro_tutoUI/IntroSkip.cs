using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSkip : MonoBehaviour
{
    public GameObject fade_in;
    public GameObject bgm;

    private void Start()
    {
        fade_in.GetComponent<Image>();
        DontDestroyOnLoad(bgm);
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            fade_in.SetActive(true);
            Invoke("SceneChange", 2.0f);
        }
    }
}
