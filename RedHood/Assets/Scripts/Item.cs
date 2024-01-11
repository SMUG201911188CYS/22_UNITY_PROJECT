using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Used,
        Gun,
        Ingredient,
        Paper,
        Projectile
    };

    public AudioClip werewolf_get_poison_sound;
   
    public Type type;
    public int value;
    Rigidbody rigid;
    SphereCollider sphereCollider;

    public Transform bulletPos;
    public GameObject bullet;
    public GameObject firecrack;
    public GameObject firecrack2;
    GameObject nearObject;

    public GameObject enemy;

    public void FirecrackerUse()
    {
        GameObject instantFireCracker = Instantiate(firecrack, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    public void FirecrackerUseTuto()
    {
        GameObject instantFireCracker = Instantiate(firecrack2, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(this.gameObject);
    }

    IEnumerator Addicted(GameObject obj/*Rigidbody rigid*/)   // 독 포션에 중독됐을 때
    {
        Enemy player = obj.GetComponent<Enemy>();
        Animator ani = player.GetComponentInChildren<Animator>();
        AudioSource sound_manager = gameObject.AddComponent<AudioSource>();
        sound_manager.volume = 0.4f;
        sound_manager.clip = werewolf_get_poison_sound;

        player.navMeshAgent.speed = 0f;
        ani.SetTrigger("posionHit");
        player.enabled = false;
        sound_manager.Play();
        //rigid.isKinematic = true;

        yield return new WaitForSeconds(5f);
        player.enabled = true;
        player.navMeshAgent.speed = 5f ;
        //rigid.isKinematic = false;
        yield return null;
    }

    public void WUse()
    {
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        //1. 총알 발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 100;
        yield return null;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        enemy = GameObject.FindWithTag("Enemy");


    }

    void OnCollisionEnter(Collision collision)  // 물리 효과 담당하는 콜라이더 회전 방지 함수. 무조건 이 콜라이더 컴포넌트를 1번째로.
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Hit" && this.gameObject.tag == "Projectile" && this.value == 2)
        {
            nearObject = GameObject.FindWithTag("Enemy");
            Debug.Log("Ouch!");
            StartCoroutine("Addicted", nearObject/*nearObject.GetComponent<Rigidbody>()*/);
            SphereCollider throwSpCol = this.gameObject.GetComponent<SphereCollider>();
            throwSpCol.enabled = false;
        }

    }



}