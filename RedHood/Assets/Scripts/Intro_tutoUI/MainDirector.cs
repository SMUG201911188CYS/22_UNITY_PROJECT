using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainDirector : MonoBehaviour
{
    public GameObject fade_out;
    public AudioClip main_sound;
    
    AudioSource sound_manager;

    private void Start()
    {
        sound_manager = gameObject.AddComponent<AudioSource>();
        fade_out.GetComponent<Image>();
        var objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length >= 1)
        {
        }
        else
        {
            sound_manager.clip = main_sound;
            sound_manager.Play();
        }
    }

    public void SceneChange()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

    public void GameStart()
    {
        fade_out.SetActive(true);
        Invoke("SceneChange", 2.0f);
    }

    public void GameEnd()
    {
        fade_out.SetActive(true);
        Invoke("Exit", 2.0f);
    }
}
