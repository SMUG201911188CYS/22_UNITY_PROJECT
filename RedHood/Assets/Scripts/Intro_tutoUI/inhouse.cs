using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inhouse : MonoBehaviour
{
    public GameObject roof1;
    public GameObject roof2;

    /*public void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            if(roof1.activeSelf == true)
            {
                roof1.SetActive(false);
                roof2.SetActive(false);
            }
            else if (roof1.activeSelf == false)
            {
                roof1.SetActive(true);
                roof2.SetActive(true);
            }
        }


    }*/

    public void OnTriggerStay(Collider player)
    {
        if (player.tag == "Player")
        {
          roof1.SetActive(false);
          roof2.SetActive(false);
        }
     }

    public void OnTriggerExit(Collider player)
    {
     if (roof1.activeSelf == false)
        {
            roof1.SetActive(true);
            roof2.SetActive(true);
        }
    }

}
