using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIitemver2 : MonoBehaviour
{
    public Player p1;
    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject uiObject3;
    public GameObject nexttrigger;
    bool isActive = true;
    Animator ani;
    void Start()
    {
        ani = p1.GetComponentInChildren<Animator>();
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        uiObject3.SetActive(false);
        nexttrigger.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((p1.hasItems[1] == true || p1.hasItems[2] == true || p1.hasItems[3] == true || p1.hasItems[4] == true) && isActive == true)
        {
            uiObject1.SetActive(true);
            uiObject2.SetActive(true);
            uiObject3.SetActive(true);
            p1.isDead = true;
            p1.speed = 0f;
            ani.SetBool("tutorialWait", true);
            isActive = false;
            p1.isBlockedFdown = true;
            StartCoroutine("WaitForSec");
            Destroy(gameObject, 14);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(11);
        p1.isDead = false;
        ani.SetBool("tutorialWait", false);
        p1.speed = 8f;
        nexttrigger.SetActive(true);
        Destroy(uiObject1);
        Destroy(uiObject2);
        Destroy(uiObject3);
    }

}
