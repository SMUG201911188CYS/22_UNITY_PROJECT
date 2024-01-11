using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIi_Game : MonoBehaviour
{
    public GameObject uiObject1;
    public GameObject uiObject2;
    bool isActive = true;

    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive == true)
        {
            uiObject1.SetActive(true);
            isActive = false;
            StartCoroutine("WaitForSec2");
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject1);
        uiObject2.SetActive(true);
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(10);
        Destroy(uiObject2);
        Destroy(gameObject);
    }

}
