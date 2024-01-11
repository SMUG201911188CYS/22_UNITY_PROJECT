using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{

    Light light;

    float minSpeed = 0.01f;
    float maxSpeed = 0.1f;
    float minInstensity = 0.1f;
    float maxIntensity = 0.8f;

    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator run()
    {
        while(true)
        {
            light.enabled = true;
            light.intensity = Random.Range(minInstensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
            light.enabled = false;
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
        }
    }
}
