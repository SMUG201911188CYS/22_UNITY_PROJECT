using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIitemver4 : MonoBehaviour
{
    public Player p1;
    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject next_trigger;

    Animator ani;

    bool isActive = true;

    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        next_trigger.SetActive(false);
        ani = p1.GetComponentInChildren<Animator>();
        p1.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (p1.hasMusket == true && p1.hasBullets >= 1 && isActive == true)
        {
            uiObject1.SetActive(true);
            uiObject2.SetActive(true);
            isActive = false;
            p1.isDead = true;
            p1.speed = 0f;
            ani.SetBool("tutorialWait", true);
            StartCoroutine("WaitForSec");
            Destroy(gameObject, 10);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(9);
        p1.isDead = false;
        p1.speed = 8f;
        ani.SetBool("tutorialWait", false);
        next_trigger.SetActive(true);
        Destroy(uiObject1);
        Destroy(uiObject2);
    }

}
