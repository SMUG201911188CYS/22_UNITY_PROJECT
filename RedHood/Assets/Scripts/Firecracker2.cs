using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker2 : MonoBehaviour {
    IEnumerator Start()
    {
        
        //Wait for 3 secs.
        yield return new WaitForSeconds(7);

        //Game object will turn off
        GameObject.Find("MeshRenderer1").SetActive(false);
    }
    //만약 늑대가 얘랑 충돌하면 Destroy하는 함수 생성

}