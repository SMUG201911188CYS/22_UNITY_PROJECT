using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Book b1;
    public GameObject text1;
    public GameObject fade_out;
    public AudioClip next_page_sound;
    float page_temp = 0;
    bool is_end_page = false;
    // Start is called before the first frame update
    
    void ReturnToMain()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    
    void Start()
    {
        fade_out.GetComponent<Image>();
        b1.GetComponent<Book>();
        text1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(b1.currentPage == 8 && is_end_page == false)
        {
            is_end_page = true;
            text1.SetActive(true);
        }

        if (is_end_page == true && Input.GetKeyDown(KeyCode.Escape))
        {
            fade_out.SetActive(true);
            Invoke("ReturnToMain", 2f);
        }

        if (page_temp != b1.currentPage)
        {
            AudioSource sound_manager = gameObject.AddComponent<AudioSource>();
            sound_manager.volume = 0.8f;
            sound_manager.clip = next_page_sound;

            sound_manager.Play();
            page_temp = b1.currentPage;
        }
    }
}
