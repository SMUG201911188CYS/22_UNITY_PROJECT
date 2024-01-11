using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIitemver : MonoBehaviour
{
    public Player p1;
    public GameObject uiObject1;
    public GameObject uiObject2;
    bool isDown = false;

    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
    }
    void Time_back()
    {
        p1.speed = 8f;
    }

    private void OnTriggerStay(Collider player)
    {
        if (isDown)
        {
            uiObject1.SetActive(true);
            uiObject2.SetActive(true);
            p1.speed = 0;
            Invoke("Time_Back", 5);
            Destroy(gameObject, 8);
        }
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        isDown = Input.GetButtonDown("Interaction");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(9);
        Destroy(uiObject1);
        Destroy(uiObject2);
    }

}
