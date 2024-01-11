using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker1 : MonoBehaviour {

    public GameObject enemy;
    
    IEnumerator Start()
    {
        
        //Wait for 3 secs.
        yield return new WaitForSeconds(7);
        enemy=GameObject.FindWithTag("Enemy");
        
        enemy.GetComponent<Enemy>().navMeshAgent.SetDestination(this.transform.position);
        enemy.GetComponent<Enemy>().m_IsFirecrackerUsed = true;
        enemy.GetComponent<Enemy>().check = true;
        Debug.Log(this.transform.position + "firecracker position");
        //Game object will turn off
        GameObject.Find("MeshRenderer1").SetActive(false);
    }
    //만약 늑대가 얘랑 충돌하면 Destroy하는 함수 생성

}