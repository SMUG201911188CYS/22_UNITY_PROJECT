using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowUI : MonoBehaviour
{

    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject street_light;
    public Player p1;

    Animator ani;

    void Start()
    {
        ani = p1.GetComponentInChildren<Animator>();
        p1.GetComponent<Player>();
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
    }


    private void OnTriggerEnter(Collider player)
    {
        p1.isTutorial = true;
        p1.isDead = true;
        p1.speed = 0f;
        ani.SetBool("tutorialWait", true);
        uiObject1.SetActive(true);
        uiObject2.SetActive(true);
        street_light.SetActive(true);
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(9);
        p1.isDead = false;
        p1.speed = 8f;
        ani.SetBool("tutorialWait", false);
        Destroy(uiObject1);
        Destroy(uiObject2);
        Destroy(gameObject);
    }
}
