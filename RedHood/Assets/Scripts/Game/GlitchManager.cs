using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class GlitchManager : MonoBehaviour
{
    public float Intensity;
    AudioSource audiosource;
    public AudioClip sds;

    void Start()
    {
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = sds;

    }

    private void OnTriggerEnter(Collider player)
    {
            if(player.tag == "Player")
            {
                audiosource.volume = 1f;//Intensity / distanceVector.magnitude;
                audiosource.Play();
            }

    }
    private void OnTriggerStay(Collider player)
    {
        if(player.tag == "Player")
        {
            Vector3 distanceVector = player.transform.position - transform.position;
            
            if(distanceVector.magnitude >= 22f)
            {
                //GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = Intensity / distanceVector.magnitude;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().scanLineJitter = Intensity / distanceVector.magnitude;
                //GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().verticalJump = 0.02f;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().horizontalShake = Intensity / distanceVector.magnitude;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = Intensity / distanceVector.magnitude;
            }
            else
            {
                //GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = Intensity / 7f;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().scanLineJitter = Intensity / 22f;
                //GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().verticalJump = 0.02f;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().horizontalShake = Intensity / 22f;
                GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = Intensity / 22f;
            }

            if(distanceVector.magnitude >= 34f && GameObject.FindWithTag("Player").GetComponent<Player>().isDead == false)
            {
                GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = Intensity / distanceVector.magnitude;
            }
            else
            {
                GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = Intensity / 34f;
            }

            if(GameObject.FindWithTag("Player").GetComponent<Player>().isDead == true)
            {
                Invoke("Dead", 2f);
            }

        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.tag == "Player")
        {
            GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = 0;
            GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().scanLineJitter = 0;
            GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().verticalJump = 0;
            GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().horizontalShake = 0;
            GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = 0;
            audiosource.Stop();
        }
        
    }

    void Dead()
    {
        Intensity += Time.deltaTime;
        GameObject.FindWithTag("MainCamera").GetComponent<DigitalGlitch>().intensity = Intensity;
        GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().scanLineJitter = Intensity;
        GameObject.FindWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = Intensity;
    }

}
