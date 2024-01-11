using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDirector : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject GamePanel;
    public GameObject fade_out;
    public static bool GamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        fade_out.GetComponent<Image>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Debug.Log("Resume");
        pauseMenu.SetActive(false);
        GamePanel.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pause");
        GamePanel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Main()
    {
        Debug.Log("Main");
        Time.timeScale = 1f;
        fade_out.SetActive(true);
        Invoke("SceneChange_Main", 2.0f);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Time.timeScale = 1f;
        fade_out.SetActive(true);
        Invoke("SceneChange", 2.0f);
    }
    public void SceneChange_Main()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void SceneChange()
    {
        Time.timeScale = 1f;
        fade_out.SetActive(true);
        Application.Quit();
    }

}
