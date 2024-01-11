using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent navMeshAgent;
    public Transform[] target; //target[0] == firecracker, target[1] ==bread , target[2] == player

    public Transform[] waypoints;

    public float speed = 1f;

    Animator ani;

    [SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 0;
    public bool isInSight;

    public bool m_IsPlayerInRange;

    public bool m_DummyInRange;

    public bool m_IsFirecrackerUsed;

    public bool check=false;

    public bool isDead=false;

    public bool isEat=false;

    public bool isLiver = false;

    public int meal = 0;

    int i=0;

    int m_CurrentWaypointIndex;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[0].position);
        ani = GetComponentInChildren<Animator>();
    }
    void Sight()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask);
        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            if (t_angle < m_angle * 0.5f)
            {
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance))
                {
                    if (t_hit.transform.name == "Player")
                    {
                        isInSight = true;

                    }
                    else
                    {
                        isInSight = false;
                    }
                }
            }
        }
    }

    void Move()
    {
        if (m_IsFirecrackerUsed)
        {
            Debug.Log(navMeshAgent.destination + "ai destination");
            navMeshAgent.speed = 10f;
            if (check &&!navMeshAgent.pathPending &&navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                ani.SetTrigger("dummyHit");
                navMeshAgent.speed = 0f;
                Debug.Log("near destination"+ this.transform.position);
                Invoke("change_bool", 3f);
            }
        } // 신호탄 사용

        else if (m_DummyInRange && m_IsFirecrackerUsed == false)
        {
            navMeshAgent.speed = 10f;


        } // 빵 사용

        else if (isInSight && m_IsPlayerInRange == false && m_DummyInRange == false && m_IsFirecrackerUsed == false)
        {
            navMeshAgent.speed = 10f * speed;
            navMeshAgent.SetDestination(target[2].position);
        } // 플레이어를 발견

        else if(isInSight==false&&m_IsPlayerInRange&&m_DummyInRange==false&&m_IsFirecrackerUsed==false){
            navMeshAgent.speed = 5f;
            navMeshAgent.SetDestination(target[2].position);
        }//플레이어 탐지

        else if (isInSight && m_IsPlayerInRange && m_DummyInRange == false && m_IsFirecrackerUsed == false)
        {
            navMeshAgent.speed = 15f;
            navMeshAgent.SetDestination(target[2].position);
        }// 플레이어를 발견 && 탐지
        if (m_IsFirecrackerUsed == false && m_DummyInRange == false && m_IsPlayerInRange == false && isInSight == false)
        {
            if (!navMeshAgent.pathPending&&navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance )
            {

                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length; 
                navMeshAgent.speed = 5f;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }

        }
        // 아무것도 없으면 패트롤

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dummy")
        {
            m_DummyInRange = true;
            navMeshAgent.SetDestination(other.transform.position);
        }
        if (other.tag == "Player")
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerInRange = false;
        }
    }

    void change_bool()
    {
        m_IsFirecrackerUsed = false;
        check = false;
        navMeshAgent.speed = 5f;
    }

    void animatonDirector(){
        if(navMeshAgent.speed<=5f && navMeshAgent.speed>0){
            ani.SetBool("isWalk",true);
            ani.SetBool("isRun",false);
        }
        if(navMeshAgent.speed>5f){
            ani.SetBool("isWalk",true);
            ani.SetBool("isRun",true);
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(isDead == false && isEat == false && isLiver == false)
        {
            animatonDirector();
            Sight();
            Move();
        }

        if(isDead&&i==0){
            navMeshAgent.speed = 0f;
            navMeshAgent.acceleration = 0f;
            navMeshAgent.SetDestination(this.transform.position);
            ani.SetBool("isWalk",false);
            ani.SetBool("isRun",false);
            ani.SetTrigger("isDead");
            i++;
        }

        if (isLiver && i == 0)
        {
            navMeshAgent.speed = 0f;
            navMeshAgent.acceleration = 0f;
            navMeshAgent.SetDestination(this.transform.position);
            i++;
        }
    }

}