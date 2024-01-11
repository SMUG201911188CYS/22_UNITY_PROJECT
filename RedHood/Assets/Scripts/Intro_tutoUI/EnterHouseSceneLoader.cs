using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterHouseSceneLoader : MonoBehaviour
{
    public GameObject fade_in;

    private void Start()
    {
        fade_in.GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider player)
    {
        fade_in.SetActive(true);
        Invoke("SceneChange", 2.0f);
    }
    public void SceneChange()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
        SceneManager.LoadScene("EnterHouseScene", LoadSceneMode.Single);
    }

}
