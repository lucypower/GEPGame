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
    private bool m_canAttack = true;

    public float m_speed = 5.0f;
    public Transform m_target;
    public float m_attackDelay;
    public Vector3 offset;
    public float m_range = 10f;

    private AIState m_state;

    private enum AIState
    {
        ATTACKING,
        ROAMING
    };

    private void Start()
    {
        m_startingPosition = transform.position;
        m_roamingPosition = GetRoamingPosition();
        m_coroutine = AttackDelay(m_attackDelay);
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

                if(m_canAttack && m_coroutine != null)
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
        return m_startingPosition + randomDirection * Random.Range(-10f, 10f);
    }    

    IEnumerator AttackDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_canAttack = true;
    }

    private void FindTarget() // finding target working (?) not moving towards target and shooting
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
            else
            {
                Debug.Log("Target not found");
                m_target = null;
            }
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

        m_canAttack = false;
        StartCoroutine(m_coroutine);

        m_shooting = GetComponent<EnemyShooting>();

        if (m_target == null)
        {
            return;
        }

        if (m_target)
        {
            if (transform.position != m_target.transform.position + offset) // problem somewhere in this bit now i think 
            {
                transform.position = m_target.transform.position + offset;
            }
            else if (transform.position == m_target.transform.position + offset)
            {
                Debug.Log("Shooting");

                m_shooting.Shooting();
            }
        }

        m_target = null;
    }


    //    private bool FindTarget()
    //    {
    //        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Flower");

    //        float closestDistance = float.MaxValue;

    //        foreach (GameObject possibleTarget in possibleTargets)
    //        {
    //            GameObject possibleTargetFlower = possibleTarget.GetComponent<GameObject>();

    //            if (possibleTargetFlower != null)
    //            {
    //                float tempDistance = (possibleTarget.transform.position - transform.position).magnitude;

    //                if (tempDistance < closestDistance)
    //                {
    //                    closestDistance = tempDistance;

    //                    m_target = possibleTargetFlower;
    //                }
    //            }

    //        }

    //        return (m_target != null);
    //    }



    //    private GameObject m_target;
    //    private NavMeshPath m_currentPath;
    //    private AIState m_state;
    //    private Vector2 m_movementAxis;
    //    [SerializeField] float m_range;

    //    private void Start()
    //    {
    //        Init();
    //    }

    //    public void Init()
    //    {
    //        transform.rotation = Quaternion.identity;

    //        m_currentPath = new NavMeshPath();

    //        NavMesh.CalculatePath(transform.position, NewWanderPoint(), NavMesh.AllAreas, m_currentPath);
    //    }

    //    private void FixedUpdate()
    //    {
    //        switch (m_state)
    //        {
    //            case AIState.WANDER:
    //                Wander();
    //                if (FindTarget())
    //                {
    //                    m_state = AIState.CHASE;
    //                }
    //                break;
    //            case AIState.CHASE:
    //                if (!Chase())
    //                {
    //                    m_state = AIState.WANDER;
    //                }
    //                break;
    //        }

    //    }

    //    private void Wander()
    //    {
    //        if (m_currentPath.corners.Length < 2)
    //        {
    //            NavMesh.CalculatePath(transform.position, NewWanderPoint(), NavMesh.AllAreas, m_currentPath);
    //        }

    //        Vector2 currentDestination = m_currentPath.corners[m_currentPath.corners.Length - 1];

    //        if ((currentDestination - (Vector2)transform.position).magnitude < 0.5f)
    //        {
    //            currentDestination = NewWanderPoint();
    //        }

    //        NavMesh.CalculatePath(transform.position, currentDestination, NavMesh.AllAreas, m_currentPath);

    //        Vector2 toNextPoint = m_currentPath.corners[1] - transform.position;

    //        m_movementAxis = toNextPoint.normalized;
    //    }

    //    private Vector2 NewWanderPoint()
    //    {
    //        return new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
    //    }


    //    private bool Chase()
    //    {
    //        if (m_target != null)
    //        {
    //            Vector2 toTarget = m_target.transform.position - transform.position;
    //            m_movementAxis = toTarget.normalized;

    //            return true;
    //        }

    //        return false;
    //    }
    //}

    //public enum AIState
    //{
    //    WANDER,
    //    CHASE
};
