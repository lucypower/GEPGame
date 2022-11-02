using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_speed;
    private Rigidbody2D m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_RB.AddForce(m_RB.transform.up * m_speed);
        Destroy(gameObject, 5f);
    }
}
