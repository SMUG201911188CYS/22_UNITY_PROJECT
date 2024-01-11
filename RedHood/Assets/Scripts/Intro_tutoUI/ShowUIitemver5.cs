using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIitemver5 : MonoBehaviour
{
    public Player p1;
    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject wall;
    Animator ani;
    bool isActive = true;

    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        ani = p1.GetComponentInChildren<Animator>();
        p1.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (p1.hasBullets == 0 && isActive == true)
        {
            uiObject1.SetActive(true);
            uiObject2.SetActive(true);
            isActive = false;
            StartCoroutine("WaitForSec");
            Destroy(gameObject, 10);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(9);
        Destroy(wall);
        Destroy(uiObject1);
        Destroy(uiObject2);
    }

}
