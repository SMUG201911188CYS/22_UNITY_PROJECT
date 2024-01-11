using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToEnd_4 : MonoBehaviour
{
    public GameObject fade_in;
    public Enemy enemy;
    public Player player;
    public AudioClip door_open;
    AudioSource manager;

    private void Start()
    {
        manager = gameObject.AddComponent<AudioSource>();
        manager.clip = door_open;
        manager.volume = 0.3f;

        enemy.GetComponent<Enemy>();
        player.GetComponent<Player>();
        fade_in.GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider oBject)
    {
        fade_in.SetActive(true);
        if(oBject.tag == "Player")
        {
            manager.Play();
            Invoke("SceneChange_end4", 2.0f);
        }
    }
    public void SceneChange_end4()
    {
        SceneManager.LoadScene("Ending4Scene", LoadSceneMode.Single);
    }
}
