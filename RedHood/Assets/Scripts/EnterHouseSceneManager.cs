using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterHouseSceneManager : MonoBehaviour
{
    public Image img;

    float onminSpeed = 0.1f;
    float onmaxSpeed = 3f;
    float offminSpeed = 0.05f;
    float offmaxSpeed = 0.3f;
    float minInstensity = 0.01f;
    float maxIntensity = 0.05f;
    public GameObject fade_out;

    void Start()
    {
        fade_out.GetComponent<Image>();
        StartCoroutine(run());
    }

    public void SceneChange()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fade_out.SetActive(true);
            Invoke("SceneChange", 2.0f);
        }
    }

    IEnumerator run()
    {
        while(true)
        {

            
            img.enabled = true;
            img.color = new Color(1, 1, 1, Random.Range(minInstensity, maxIntensity));
            yield return new WaitForSeconds(Random.Range(offminSpeed, offmaxSpeed));
            img.enabled = false;
            yield return new WaitForSeconds(Random.Range(onminSpeed, onmaxSpeed));
        }
    }
}
