using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToEnd_35 : MonoBehaviour
{
    public GameObject fade_in;
    public Enemy enemy;
    public Player player;
    public AudioClip redhood_caught_sound;
    public AudioClip werewolf_dying_sound;
    public AudioClip werewolf_caught_sound;
    AudioSource sound_manager;
    AudioSource sound_manager2;

    bool end_check = true;
    
    private void Start()
    {
        enemy.GetComponent<Enemy>();
        player.GetComponent<Player>();
        fade_in.GetComponent<Image>();
        sound_manager = gameObject.AddComponent<AudioSource>();
        sound_manager.volume = 0.4f;
        sound_manager2 = gameObject.AddComponent<AudioSource>();
        sound_manager2.volume = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead == true && end_check)
        {
            end_check = false;
            sound_manager.clip = redhood_caught_sound;
            sound_manager.Play();
            sound_manager2.clip = werewolf_caught_sound;
            sound_manager2.Play();
            StartCoroutine("WaitForSec");
            Invoke("SceneChange_end5", 4f);
        }

        if (enemy.isDead == true && end_check)
        {
            sound_manager.volume = 1f;
            sound_manager.clip = werewolf_dying_sound;
            sound_manager.Play();
            player.enabled = false;
            end_check = false;
            StartCoroutine("WaitForSec");
            Invoke("SceneChange_end3", 4f);
        }
    }

    public void Fade_out_active()
    {
        fade_in.SetActive(true);
    }
    public void SceneChange_end3()
    {

        SceneManager.LoadScene("Ending3Scene", LoadSceneMode.Single);
    }

    public void SceneChange_end5()
    {
        SceneManager.LoadScene("Ending5Scene", LoadSceneMode.Single);
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        fade_in.SetActive(true);
    }
}

