using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSound : MonoBehaviour
{
    public AudioClip footSound;
    AudioSource manager;

    private void Start()
    {
        manager = gameObject.AddComponent<AudioSource>();
        manager.clip = footSound;
        manager.volume = 0.1f;
    }


    void OnTriggerEnter(Collider _col)
    {

        if (_col.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            manager.Play();
        }
    }
}
