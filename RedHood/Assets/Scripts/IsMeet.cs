using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMeet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public AudioClip werewolf_get_bread_sound;
    public GameObject player;
    Animator ani;

    Animator p_ani;
    void Start()
    {
        ani = enemy.GetComponentInChildren<Animator>();
        p_ani = player.GetComponentInChildren<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dummy")
        {
            AudioSource sound_manager = gameObject.AddComponent<AudioSource>();
            sound_manager.volume = 0.4f;
            sound_manager.clip = werewolf_get_bread_sound;
            enemy.GetComponent<Enemy>().navMeshAgent.speed = 0f;
            Destroy(other.gameObject);
            ani.SetTrigger("dummyHit");
            enemy.GetComponent<Enemy>().meal++;
            sound_manager.Play();
            Invoke("change_bool", 3f);


        }
        if (other.tag == "Player")
        {
            enemy.GetComponent<Enemy>().isEat = true;

            enemy.GetComponent<Animator>().SetTrigger("isEat");
            enemy.GetComponent<Animator>().SetBool("isRun", false);
            enemy.GetComponent<Animator>().SetBool("isWalk", false);
            p_ani.SetBool("isRun",false);
            p_ani.SetBool("isDead", true);
            player.GetComponent<Player>().isDead = true;
            player.GetComponent<Player>().speed = 0f;

        }
    }
    void change_bool()
    {
        enemy.GetComponent<Enemy>().m_DummyInRange = false;
    }

}
