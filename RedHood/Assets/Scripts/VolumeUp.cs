using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUp : MonoBehaviour
{
    public float animTime = 2f;
    public Camera camera; 

    private float start = 0f;
    private float end = 1f;
    private float time = 0f; 

    private AudioSource audio;
    // Start is called before the first frame update
    void Awake()
    {
        audio = camera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlVolume();
    }

    void ControlVolume()
    {
        time += Time.deltaTime / animTime;

        audio.volume = Mathf.Lerp(start, end, time);
    }
}
