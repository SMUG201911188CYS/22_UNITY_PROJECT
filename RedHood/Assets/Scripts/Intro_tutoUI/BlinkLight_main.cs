using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight_main : MonoBehaviour
{

    Light light1;

    float minSpeed = 0.1f;
    float maxSpeed = 0.4f;
    float minInstensity = 0.6f;
    float maxIntensity = 1.0f;

    void Start()
    {
        light1 = GetComponent<Light>();
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
            light1.enabled = true;
            light1.intensity = Random.Range(minInstensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
            light1.enabled = false;
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
        }
    }
}
