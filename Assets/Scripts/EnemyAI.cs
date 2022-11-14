using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Vector2 m_startingPosition;
    private Vector2 m_roamingPosition;
    private EnemyShooting m_shooting;
    private IEnumerator m_coroutine;
    private bool m_canAttack;

    public float m_speed = 5.0f;
    public Transform m_target;
    public float m_attackDelay;
    public Vector3 offset;
    public float m_range = 10f;

    public GameObject m_prefab;
    public Transform m_spawnPosition;


    private AIState m_state;

    private enum AIState
    {
        ATTACKING,
        ROAMING
    };

    private void Start()
    {
        //m_canAttack = false;
        m_coroutine = ShootingDelay(m_attackDelay);
        StartCoroutine(m_coroutine);

        m_startingPosition = transform.position;
        m_roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {      
        switch (m_state)
        {
            case AIState.ROAMING:

                Roaming();

                if(m_canAttack /*&& m_coroutine != null*/)
                {
                    FindTarget();

                    if (m_target)
                    {
                        Debug.Log("Attack State Started");
                        m_state = AIState.ATTACKING;
                    }                                        
                }

                break;

            case AIState.ATTACKING:

                Attack();

                if(!m_canAttack)
                {
                    m_state = AIState.ROAMING;
                }
                                
                break;
        }
    }

    private void Roaming()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_roamingPosition, m_speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, m_roamingPosition) < 1f)
        {
            m_roamingPosition = GetRoamingPosition();
        }
    }

    private Vector2 GetRoamingPosition()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return m_startingPosition + randomDirection * Random.Range(-5f, 5f);
    }   

    private void FindTarget() 
    {
        Debug.Log("Target Searching");

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Flower");

        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= m_range)
            {
                Debug.Log("Target Found");

                m_target = target.transform;
            }
            //else
            //{
            //    Debug.Log("Target not found");
            //    m_target = null;
            //}
        }
        
        //if (!m_target)
        //{
        //    return false;
        //}

        //return true;
    }



    private void Attack() 
    {

        Debug.Log("Attacking");

        if (m_target)
        {
            if (transform.position != m_target.transform.position + offset) // problem somewhere in this bit now i think 
            {
                Debug.Log("Moving");
                transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position + offset, m_speed * Time.deltaTime);
            }

            if (transform.position == m_target.transform.position + offset)
            {
                Debug.Log("Shooting");

                Shooting();
                m_target = null;
            }
        }
    }

    public void Shooting()
    {
        // if not shooting and coroutine is null then instantiate bullet and say enemy shooting and start coroutine
        if (m_canAttack && m_coroutine != null)
        {
            Instantiate(m_prefab, m_spawnPosition.position, m_spawnPosition.rotation);
            m_canAttack = false;
            m_coroutine = ShootingDelay(m_attackDelay);
            StartCoroutine(m_coroutine);
        }
    }

    IEnumerator ShootingDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_canAttack = true;
    }    
};
