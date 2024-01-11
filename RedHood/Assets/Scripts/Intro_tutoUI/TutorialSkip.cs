using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialSkip : MonoBehaviour
{
    public GameObject fade_in;

    private void Start()
    {
        fade_in.GetComponent<Image>();
    }
    public void SceneChange()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
        SceneManager.LoadScene("EnterHouseScene", LoadSceneMode.Single);
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
