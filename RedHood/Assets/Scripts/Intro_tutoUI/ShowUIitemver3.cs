using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIitemver3 : MonoBehaviour
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

        if (p1.hasItems[1] == true && p1.hasItems[2] == true && p1.hasItems[3] == true && p1.hasItems[4] == true)
        {
            p1.isBlockedFdown = false;
        }
        if (p1.hasItems[1] == false && p1.hasItems[2] == false && p1.hasItems[3] == false && p1.hasItems[4] == false && isActive == true)
        {
            uiObject1.SetActive(true);
            uiObject2.SetActive(true);
            isActive = false;
            p1.isDead = true;
            p1.speed = 0f;
            Debug.Log(p1.speed);
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
        Destroy(wall);
        Destroy(uiObject1);
        Destroy(uiObject2);
    }

}
