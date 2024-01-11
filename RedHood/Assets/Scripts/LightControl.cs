using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    void OnTriggerStay(Collider other){
        if(other.tag == "Light"){
            other.GetComponent<BlinkLight>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Light"){
            other.GetComponent<BlinkLight>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
