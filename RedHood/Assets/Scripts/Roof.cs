using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MonoBehaviour
{
    public MeshRenderer roof;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        roof = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
            roof.enabled = false;
    }
    void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
            roof.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
