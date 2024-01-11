using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToEnd_126 : MonoBehaviour
{
    public GameObject fade_in;
    public Enemy enemy;
    public Player player;
    private void Start()
    {
        enemy.GetComponent<Enemy>();
        player.GetComponent<Player>();
        fade_in.GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider oBject)
    {
        fade_in.SetActive(true);
        if(oBject.tag == "Player")
        {
            enemy.isLiver = true;
            if (player.hasAllPapers == true)
            {
                Invoke("SceneChange_end6", 2.0f);
            }
            else if (enemy.meal >= 3)
            {
                Invoke("SceneChange_end1", 2.0f);
            }
            else
            {
                Invoke("SceneChange_end2", 2.0f);
            }
        }
    }
    public void SceneChange_end1()
    {
        SceneManager.LoadScene("Ending1Scene", LoadSceneMode.Single);
    }
    public void SceneChange_end2()
    {
        SceneManager.LoadScene("Ending2Scene", LoadSceneMode.Single);
    }
    public void SceneChange_end6()
    {
        SceneManager.LoadScene("Ending6Scene", LoadSceneMode.Single);
    }
}
