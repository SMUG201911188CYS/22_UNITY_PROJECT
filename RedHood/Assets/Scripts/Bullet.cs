using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor") {
            Destroy(gameObject, 3);
        }
        else if(collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
        
    }
    
    void OnTriggerEnter(Collider other){
    if (other.tag == "Hit"){
            GameObject enemy;
            enemy = GameObject.FindWithTag("Enemy");
            Enemy ene;
            ene = enemy.GetComponent<Enemy>();
            Animator ani;
            ani = enemy.GetComponentInChildren<Animator>();
            
            ene.isDead = true;

            
            Destroy(gameObject);
        }
    }
}

